using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Exceptions;
using Aether.QualityChecks.Extensions.MethodExtensions;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Aether.QualityChecks.Helpers
{
    public class QualityCheckExecutionHandler : IQualityCheckExecutionHandler
    {
        private LinkedList<QualityCheckResponseModel> responses;

        public async Task<List<QualityCheckResponseModel>> ExecuteQualityCheck(IQualityCheck qc)
        {
            var type = qc.GetType();

            responses = new LinkedList<QualityCheckResponseModel>();

            var methods = type.GetMethods();

            IEnumerable<QualityCheckInitializeDataAttribute> initDataMethods = null;
            MethodInfo initMethod = type.GetMethodWithAttribute<QualityCheckInitializeAttribute>();

            if (initMethod != null)
                initDataMethods = initMethod.GetAttribute<QualityCheckInitializeDataAttribute>();

            IEnumerable<MethodInfo> stepMethods = type.GetMethodsWithAttribute<QualityCheckStepAttribute>();
            MethodInfo teardownMethod = type.GetMethodWithAttribute<QualityCheckTearDownAttribute>();

            if (initDataMethods != null && initDataMethods.Any())
            {
                foreach(var initDataMethod in initDataMethods)
                {
                    responses.AddLast(new QualityCheckResponseModel(type.Name) { InitializeData = initDataMethod.Seeds });
                    await TryExecuteQC(qc, initMethod, stepMethods, teardownMethod, initDataMethod.Seeds);
                }
            }
            else
            {
                responses.AddLast(new QualityCheckResponseModel(type.Name));
                await TryExecuteQC(qc, initMethod, stepMethods, teardownMethod);
            }


            return responses.ToList();
        }

        private async Task TryExecuteQC(IQualityCheck qc, MethodInfo initMethod, IEnumerable<MethodInfo> stepMethods, MethodInfo teardownMethod, object[] initData = null)
        {
            try
            {
                await ExecuteSingleMethodInfo(qc, initMethod, initData);

                await ExecuteSteps(qc, stepMethods);
            }
            catch(TargetParameterCountException e)
            {
                StepResponse paramFailure = new StepResponse("Param Failure") { Message = "One or more of your methods has a mistmatch between their Data Attributes, and the params provided to the method"};
                responses.Last.Value.Steps.Add(paramFailure);
            }
            finally
            {
                try
                {
                    await ExecuteSingleMethodInfo(qc, teardownMethod);
                }
                catch (Exception e)
                {
                    StepResponse criticalFailure = new StepResponse(nameof(ExecuteSingleMethodInfo) + "-TearDown") { Message = "Tear Down Failed Catastrophically", Exception = e };
                    responses.Last.Value.Steps.Add(criticalFailure);
                }
            }
        }

        private async Task ExecuteSteps(IQualityCheck qc, IEnumerable<MethodInfo> methods)
        {
            var stepMethodsInfo = methods.Where(m => m.GetCustomAttributes(typeof(QualityCheckStepAttribute), true).Any());
            List<(int, MethodInfo)> stepsWithOrder = new List<(int, MethodInfo)>();

            foreach (var stepMethod in stepMethodsInfo)
            {
                var atr = Attribute.GetCustomAttribute(stepMethod, typeof(QualityCheckStepAttribute)) as QualityCheckStepAttribute;

                stepsWithOrder.Add((atr.Order, stepMethod));
            }

            bool testFailed = false;

            foreach (var s in stepsWithOrder.OrderBy(s => s.Item1).Select(s => s.Item2))
            {
                if (testFailed)
                    break;

                var dataAttributes = s.GetAttribute<QualityCheckDataAttribute>();

                if(dataAttributes is null || !dataAttributes.Any())
                {
                    var sr = await InvokeStep(qc, s, null);

                    if (sr != null)
                    {
                        responses.Last.Value.Steps.Add(sr);
                        testFailed = !sr.StepPassed;
                    }
                }
                else
                {
                    foreach(var da in dataAttributes)
                    {
                        if (testFailed)
                            break;

                        var subSr = await InvokeStep(qc, s, da.Parameters);

                        if (subSr != null)
                        {
                            responses.Last.Value.Steps.Add(subSr);

                            testFailed = !subSr.StepPassed;
                        }
                    }
                }
            }

        }

        private static async Task<StepResponse> InvokeStep(IQualityCheck qc, MethodInfo s, object[] parameters)
        {
            StepResponse sr = new StepResponse(s.Name);
            sr.StepPassed = true;
            sr.DataInput = parameters;

            try
            {
                try
                {
                    if (s.ReturnType == typeof(Task))
                        await (Task)s.Invoke(qc, parameters);
                    else
                        s.Invoke(qc, parameters);
                }
                catch(TargetInvocationException e)
                {
                    throw e.InnerException;
                }
            }
            catch(Exception e)
            {
                if(e is StepSuccessException)
                {
                    StepSuccessException success = e as StepSuccessException;

                    sr.StepPassed = true;
                    sr.Message = success.Message;
                    sr.DataResponse = success.DataObject;
                    
                }
                else if(e is StepWarnException)
                {
                    StepWarnException warning = e as StepWarnException;

                    sr.StepPassed = true;
                    sr.Message = warning.Message;
                    sr.Exception = warning.InnerException;
                    sr.DataResponse = warning.DataObject;
                }
                else if(e is StepFailedException)
                {

                    StepFailedException failure = e as StepFailedException;

                    sr.StepPassed = false;
                    sr.Message = failure.Message;
                    sr.Exception = failure.InnerException;
                    sr.DataResponse = failure.DataObject;
                }
                else
                {
                    sr.StepPassed = false;
                    sr.Message = e.Message;
                    sr.Exception = e;
                }

            }

            return sr;
        }

        public static async Task ExecuteMethodInfos(IQualityCheck qc, IEnumerable<MethodInfo> methods, object[] parameters = null)
        {
            foreach (var method in methods)
            {
                await ExecuteSingleMethodInfo(qc, method, parameters);
            }
        }

        private static async Task ExecuteSingleMethodInfo(IQualityCheck qc, MethodInfo method, object[] parameters = null)
        {
            if (method != null)
            {
                if (method.ReturnType == typeof(Task))
                    await (Task)method.Invoke(qc, parameters);
                else
                    method.Invoke(qc, parameters);
            }
        }
    }
}

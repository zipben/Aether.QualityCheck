﻿using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Exceptions;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aether.QualityChecks.Helpers
{
    public class QualityCheckExecutionHandler : IQualityCheckExecutionHandler
    {
        private QualityCheckResponseModel response;

        public async Task<QualityCheckResponseModel> ExecuteQualityCheck(IQualityCheck qc)
        {
            var type = qc.GetType();

            response = new QualityCheckResponseModel(type.Name);

            var methods = type.GetMethods();

            try
            {
                await ExecuteInitialize(qc, methods);

                await ExecuteSteps(qc, methods);
            }
            finally
            {
                try
                {
                    await ExecuteTeardown(qc, methods);
                }
                catch(Exception e)
                {
                    StepResponse criticalFailure = new StepResponse(nameof(ExecuteTeardown)) { Message = "Tear Down Failed Catastrophically", Exception = e };
                    response.Steps.Add(criticalFailure);
                }
            }

            return response;
        }

        private static async Task ExecuteInitialize(IQualityCheck qc, MethodInfo[] methods)
        {
            var initMethodsInfo = methods.Where(m => m.GetCustomAttributes(typeof(QualityCheckInitializeAttribute), true).Any()).ToList();

            await ExecuteMethodInfos(qc, initMethodsInfo);

        }

        private async Task ExecuteSteps(IQualityCheck qc, MethodInfo[] methods)
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

                var dataAttributes = Attribute.GetCustomAttributes(s, typeof(QualityCheckDataAttribute)).Select(a => a as QualityCheckDataAttribute);

                if(dataAttributes is null || !dataAttributes.Any())
                {
                    var sr = await InvokeStep(qc, s, null);

                    if (sr != null)
                    {
                        response.Steps.Add(sr);
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
                            response.Steps.Add(subSr);

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

        private async static Task ExecuteTeardown(IQualityCheck qc, MethodInfo[] methods)
        {
            var teardownMethodsInfo = methods.Where(m => m.GetCustomAttributes(typeof(QualityCheckTearDownAttribute), true).Any()).ToList();

            await ExecuteMethodInfos(qc, teardownMethodsInfo);
        }

        public static async Task ExecuteMethodInfos(IQualityCheck qc, List<MethodInfo> methods)
        {
            foreach (var s in methods)
            {
                if (s.ReturnType == typeof(Task))
                    await (Task)s.Invoke(qc, null);
                else
                    s.Invoke(qc, null);
            }
        }
    }
}
using Aether.QualityChecks.Attributes;
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
            response = new QualityCheckResponseModel(qc.LogName);

            var methods = qc.GetType().GetMethods();

            await ExecuteInitialize(qc, methods);

            await ExecuteSteps(qc, methods);

            await ExecuteTeardown(qc, methods);

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

            foreach (var s in stepsWithOrder.OrderBy(s => s.Item1).Select(s => s.Item2))
            {
                StepResponse sr = null;

                try
                {
                    if (s.ReturnType == typeof(Task<StepResponse>))
                        sr = await (Task<StepResponse>)s.Invoke(qc, null);
                    else
                        sr = (StepResponse)s.Invoke(qc, null);
                }
                catch(Exception e)
                {
                    if (sr == null)
                    {
                        sr = new StepResponse();
                        sr.Name = s.Name;
                        sr.Exception = e;
                        sr.Message = e.Message;
                        sr.StepPassed = false;
                    }
                }


                if (sr != null)
                    response.Steps.Add(sr);
            }

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

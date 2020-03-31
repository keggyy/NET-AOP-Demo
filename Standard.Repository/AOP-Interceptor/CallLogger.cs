using Castle.DynamicProxy;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Repository.AOP_Interceptor
{
    public class CallLogger : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var executionId = Guid.NewGuid().ToString();

            Debug.WriteLine("{0} - Calling method {1} with parameters {2}",
                executionId,
               invocation.Method.Name,
               JsonConvert.SerializeObject(invocation.Arguments));
            try
            {
                invocation.Proceed();
                if (invocation.ReturnValue is Task taskResult)
                {
                    taskResult.ContinueWith(
                        t =>
                            {
                                try
                                {
                                    var _taskType = t.GetType();

                                    Debug.WriteLine("{0} - Done: result was {1}.",
                                    executionId,
                                    JsonConvert.SerializeObject(_taskType.GetProperty("Result").GetValue(t)));
                                }
                                catch { }
                            }, TaskContinuationOptions.None);

                }
                else
                {
                    Debug.WriteLine("{0} - Done: result was {1}.",
                    executionId,
                   JsonConvert.SerializeObject(invocation.ReturnValue));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{executionId} - {ex.ToString()}");
                throw;
            }
        }
    }
}
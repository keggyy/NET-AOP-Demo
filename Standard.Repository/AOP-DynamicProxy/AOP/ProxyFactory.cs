using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.AOP_DynamicProxy.AOP
{
    public class ProxyFactory<T> : DispatchProxy
    {
        private T _decorated;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            var interfaceMethods = _decorated.GetType().GetMethods();

            var annotations = targetMethod.GetCustomAttributes(true);
            var aopAttr = annotations.FirstOrDefault(x => typeof(AOPAttribute).IsAssignableFrom(x.GetType())) as AOPAttribute;

            if (aopAttr != null)
            {
                var res = aopAttr.Execute(targetMethod, args, annotations);
                return res;
            }

            var inherithedMethod = interfaceMethods.FirstOrDefault(x => x.Name == targetMethod.Name);
            var result = inherithedMethod.Invoke(_decorated, args);
            return result;
        }

        public static T Create<T, TProxy>(TProxy instance) where T : class where TProxy : class
        {
            object proxy = Create<T, ProxyFactory<TProxy>>();
            ((ProxyFactory<TProxy>)proxy).SetParameters(instance);

            return (T)proxy;
        }

        private void SetParameters(T decorated)
        {
            _decorated = decorated;
        }
    }
}

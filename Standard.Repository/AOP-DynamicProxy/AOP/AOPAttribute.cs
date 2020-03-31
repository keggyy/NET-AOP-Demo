using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.AOP_DynamicProxy.AOP
{
    public abstract class AOPAttribute : Attribute
    {
        public abstract object Execute(MethodInfo targetMethod, object[] args, object[] annotations);
    }
}

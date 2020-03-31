using Autofac.Core;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.AOP_DynamicProxy.AOP
{
    public class QueryAttribute : AOPAttribute
    {
        private string query { get; set; }

        public QueryAttribute(string sqlQuery)
        {
            this.query = sqlQuery;
        }
        public override object Execute(MethodInfo targetMethod, object[] args, object[] annotations)
        {
            using (var context = new DemoContext())
            {
                return context.Books.FromSqlRaw(this.query, args).ToList();
            }
        }
    }
}

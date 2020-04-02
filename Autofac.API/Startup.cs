using Autofac.API.Controllers.Api;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Repository.AOP_Interceptor;
using System.Linq;

namespace Autofac.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddControllersAsServices();
            services.AddAuthorization();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Web API", Version = "v1" });
                c.IgnoreObsoleteProperties();
                c.IgnoreObsoleteActions();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.CustomSchemaIds(t => t.FullName);
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.Register(c => new CallLogger())
            .Named<IInterceptor>("logs");

            builder.RegisterType<BookController>().InstancePerLifetimeScope().EnableClassInterceptors();
            builder.RegisterType<BookRepository>().As<IBookRepository>().InstancePerLifetimeScope().EnableInterfaceInterceptors();
            builder.RegisterType<DemoContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseWelcomePage();
        }
    }
}
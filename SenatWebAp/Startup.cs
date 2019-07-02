using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Newtonsoft.Json.Converters;
using System.Net.Http.Formatting;
using System.Reflection;
using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json.Serialization;
using NLog;
using SenatWebAp.Infrastructure;
using SenatWebAp.Controller;
using SenatWebAp.Validation;
using Swashbuckle.Application;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using SenatApi;
using Hangfire;
using GlobalConfiguration = Hangfire.GlobalConfiguration;


namespace SenatWebAp
{
    
    public class Startup
    {
       
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            ConfigCookie(app);
            config.EnableSwagger(c => { c.SingleApiVersion("v1", "WebAPI");}).EnableSwaggerUi();
            ConfigureWebApi(config);
            app.Use(typeof(LoggingMiddleware));
            app.CreatePerOwinContext<SenatRoleManager>(SenatRoleManager.Create);
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<RestSenatApiClient_>()
                .WithParameter("baseUrl", "https://dev.senat.sbt-osop-224.sigma.sbrf.ru").SingleInstance();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
            HangdireJob(app);
            app.UseWebApi(config);
            
        }

        private void ConfigCookie(IAppBuilder app)
        {
            app.CreatePerOwinContext(SenatDbContext.Create);
            app.CreatePerOwinContext<SenatUserManager>(SenatUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });
            
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver= new CamelCasePropertyNamesContractResolver();
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter
            {
                SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                    Formatting = Newtonsoft.Json.Formatting.Indented
                }
            });
            config.Formatters.JsonFormatter.SerializerSettings.Converters
                .Add(new StringEnumConverter());
            config.Filters.Add(new ValidationActionFilter());
            

        }

        private void HangdireJob(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            app.UseHangfireDashboard();
            RecurringJob.AddOrUpdate(
                () => Console.WriteLine("How'r u?!"),
                Cron.Minutely);
            app.UseHangfireServer();
        }
    }
}

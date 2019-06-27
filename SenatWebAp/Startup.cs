using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Newtonsoft.Json.Converters;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using NLog;
using SenatWebAp.Infrastructure;
using SenatWebAp.Controller;


namespace SenatWebAp
{
    
    public class Startup
    {
       
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            ConfigOAuthTokenGeneration(app);
            ConfigureWebApi(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.Use(typeof(LoggingMiddleware));
            app.UseWebApi(config);
            app.Use(async (context, next) =>
            {
                if (!context.Request.Path.Value.Contains("/api"))
                    await context.Response.WriteAsync("Start Page!");
                else
                    await next.Invoke();
                
            });
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
        }

        private void ConfigOAuthTokenGeneration(IAppBuilder app)
        {
            app.CreatePerOwinContext(SenatDbContext.Create);
            app.CreatePerOwinContext<SenatUserManager>(SenatUserManager.Create);
        }

        private void ConfigureWebApi(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
            var jsonFormatter = configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver= new CamelCasePropertyNamesContractResolver();
        }
    }
}

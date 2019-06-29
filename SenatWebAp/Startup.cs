using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Newtonsoft.Json.Converters;
using System.Net.Http.Formatting;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
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
            ConfigCookie(app);
            ConfigureWebApi(config);
            app.Use(typeof(LoggingMiddleware));
            app.UseWebApi(config);
            app.Use(async (context, next) => // перенести
            {
                if (!context.Request.Path.Value.Contains("/api"))
                    await context.Response.WriteAsync("Start Page!");
                else
                    await next.Invoke();
            });
        }

        private void ConfigCookie(IAppBuilder app)
        {
            app.CreatePerOwinContext<SenatDbContext>(SenatDbContext.Create);
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
        }
    }
}

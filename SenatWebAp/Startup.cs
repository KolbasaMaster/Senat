using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Newtonsoft.Json.Converters;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using NLog;

namespace SenatWebAp
{
    
    public class Startup
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });


            app.Use(async (context, next) =>
            {
                
                await next.Invoke();
                if (context.Request.Path.Value.Contains("/api"))
                {
                    context.Response.Headers.Add("Hello", new[] { "World" });
                }
            });


            app.Use(async (context, next) =>
            {
                var request = context.Request.Body;
                logger.Trace(request);
                var respounse = context.Response.Body;
                logger.Trace(respounse);
                await next.Invoke();
            });
            app.UseWebApi(config);
            app.Use(async (context, next) =>
            {
                if (!context.Request.Path.Value.Contains("/api"))
                {
                    await context.Response.WriteAsync("Start Page!");
                }
                else
                {
                    await next.Invoke();
                }
            });

            config.Formatters.Clear();
           

            config.Formatters.Add(new JsonMediaTypeFormatter
            {
                SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                    Formatting = Newtonsoft.Json.Formatting.Indented
                }
            });

            config.Formatters.JsonFormatter.SerializerSettings.Converters
                .Add(new StringEnumConverter());
        }
    }
}

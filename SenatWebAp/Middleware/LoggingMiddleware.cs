using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using NLog;
using System.Threading.Tasks;

namespace SenatWebAp
{
    public class LoggingMiddleware : OwinMiddleware
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public LoggingMiddleware(OwinMiddleware next)
            : base(next)
        {

        }

        public async override Task Invoke(IOwinContext context)
        {
            var request = context.Request.Body;
            logger.Trace(request);
            await Next.Invoke(context);
            var respounse = context.Response.Body;
            logger.Trace(respounse);
        }
    }
}
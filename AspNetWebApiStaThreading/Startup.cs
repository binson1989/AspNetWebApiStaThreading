using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using NLog.Owin.Logging;

namespace AspNetWebApiStaThreading
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new ValidateModelStateAttribute());
            config.Services.Replace(typeof(IHttpActionInvoker), new StaThreadEnabledHttpActionInvoker());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalHandler());

            //appBuilder.Use
            //appBuilder.UseNLog();
            appBuilder.UseWebApi(config);
        }
    }
}

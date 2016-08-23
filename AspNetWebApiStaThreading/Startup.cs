using Owin;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using FluentValidation.WebApi;
using FluentValidation;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Extras.NLog;

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
            config.Filters.Add(new ValidateModelStateAttribute());
            config.MapHttpAttributeRoutes();
            
            //config.Services.Replace(typeof(IHttpActionInvoker), new StaThreadEnabledHttpActionInvoker());
            //config.Services.Replace(typeof(IExceptionHandler), new GlobalHandler());

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType(typeof(GoodService)).As<IGoodService>().SingleInstance();
            builder.RegisterType(typeof(StaThreadEnabledHttpActionInvoker)).As<IHttpActionInvoker>().SingleInstance();
            builder.RegisterType(typeof(GlobalHandler)).As<IExceptionHandler>().SingleInstance();
            builder.RegisterModule<NLogModule>();
            //builder.RegisterInstance(new GoodService()).As<IGoodService>();

            var validators = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly());
            validators.ForEach(result => builder.RegisterType(result.ValidatorType).As(result.InterfaceType).SingleInstance());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            FluentValidationModelValidatorProvider.Configure(config, provider => provider.ValidatorFactory = new AutofacValidatorFactory(container));

            //appBuilder.Use
            //appBuilder.UseNLog();
            appBuilder.UseAutofacMiddleware(container)
                .UseAutofacWebApi(config)
                .UseWebApi(config);
        }
    }
}

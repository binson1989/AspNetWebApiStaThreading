using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;
using System.Web.Http.Controllers;

namespace AspNetWebApiStaThreading
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Web server started !!!");
                Console.ReadLine();
            }
        }
    }

    public static class TaskFactoryExtensions
    {
        private static readonly TaskScheduler _staScheduler = new StaTaskScheduler(numberOfThreads: 1);

        public static Task<TResult> StartNewSta<TResult>(this TaskFactory factory, Func<TResult> action)
        {
            return factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, _staScheduler);
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class UseStaThreadAttribute : Attribute { }

    public class StaThreadEnabledHttpActionInvoker : ApiControllerActionInvoker
    {
        public override Task<HttpResponseMessage> InvokeActionAsync(HttpActionContext context, CancellationToken cancellationToken)
        {
            // Determine whether action has attribute UseStaThread
            bool useStaThread = context.ActionDescriptor.GetCustomAttributes<UseStaThreadAttribute>().Any();

            // If it doesn't, simply return the result of the base method
            if (!useStaThread)
            {
                return base.InvokeActionAsync(context, cancellationToken);
            }

            // Otherwise, create an STA thread and then call the base method
            Task<HttpResponseMessage> responseTask = Task.Factory.StartNewSta(() => base.InvokeActionAsync(context, cancellationToken).Result);

            return responseTask;
        }
    }
}

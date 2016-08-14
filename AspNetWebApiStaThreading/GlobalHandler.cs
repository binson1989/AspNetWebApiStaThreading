using System;
using System.Diagnostics;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace AspNetWebApiStaThreading
{
    public class GlobalHandler : ExceptionHandler
    {
        public GlobalHandler()
        {
            Debug.WriteLine("XXX: Exception Handler instance created!!!");
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            Console.WriteLine("Inside global exception handler");
            context.Result = new InternalServerErrorResult(context.Request);
        }
    }
}
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;

namespace AspNetWebApiStaThreading
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
       HttpStatusCode.BadRequest, actionContext.ModelState);
            }

            //base.OnActionExecuting(actionContext);
            //var requestScope = actionContext.Request.GetDependencyScope();


            //var parameters = actionContext.ActionArguments.Select(x => x.Value).Where(x => x != null);

            //foreach (var parameter in parameters)
            //{
            //    var argumentType = parameter.GetType();
            //    var validator = FindValidator(argumentType);
            //    if (validator != null)
            //    {
            //        var validationResult = validator.Validate(parameter);
            //        if (!validationResult.IsValid)
            //        {
            //            ThrowFormattedApiResponse(validationResult);
            //        }
            //    }
            //}
        }

        //private IValidator FindValidator(Type type, IDependencyScope depScope)
        //{

        //    // Can be replaced with custom IOC logic
        //    if (type == typeof(Company))
        //    {
        //        depScope.GetService()
        //    }
        //    return null;
        //}

        //private void ThrowFormattedApiResponse(ValidationResult validationResult)
        //{
        //    var errorsModel = new ErrorsModel();

        //    var formattedErrors = validationResult.Errors.Select(x =>
        //    {
        //        var errorModel = new ErrorModel();
        //        var errorState = x.CustomState as ErrorState;
        //        if (errorState != null)
        //        {
        //            errorModel.ErrorCode = errorState.ErrorCode;
        //            errorModel.Field = x.PropertyName;
        //            errorModel.Documentation = "https://developer.example.com/docs" + errorState.DocumentationPath;
        //            errorModel.DeveloperMessage = string.Format(errorState.DeveloperMessageTemplate, x.PropertyName);

        //            // Can be replaced by translating a localization key instead
        //            // of just mapping over a hardcoded message
        //            errorModel.UserMessage = errorState.UserMessage;
        //        }
        //        return errorModel;
        //    });
        //    errorsModel.Errors = formattedErrors;

        //    var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
        //    {
        //        Content = new StringContent(JsonConvert.SerializeObject(errorsModel, Formatting.Indented))
        //    };
        //    throw new HttpResponseException(responseMessage);
        //}
    }
}

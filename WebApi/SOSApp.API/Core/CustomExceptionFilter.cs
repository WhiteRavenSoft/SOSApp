using SOSApp.Core.Enum;
using SOSApp.Data.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace SOSApp.API.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {

            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;
            var exceptionType = actionExecutedContext.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Access to the Web API is not authorized.";
                status = HttpStatusCode.Unauthorized;
            }

            else if (exceptionType == typeof(NullReferenceException))
            {
                message = "Internal Server Error. " + actionExecutedContext.Exception.Message;
                status = HttpStatusCode.InternalServerError;
            }
            else
            {
                message = "Not found. " + actionExecutedContext.Exception.Message;
                status = HttpStatusCode.NotFound;
            }


            //TODO Log error

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(status, new AppResponse<object>() { Code = (int)AppErrorCodeEnum.Generic, Message = message });
            base.OnException(actionExecutedContext);
        }

    }
}
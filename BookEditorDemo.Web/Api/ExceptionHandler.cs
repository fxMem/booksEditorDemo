using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;


namespace BookEditorDemo.Web.Api
{
    public class ApiExceptionHandler: ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new JsonResult<ApiResult>(
                ResultBuilder.Error().WithMessage($"Internal server error: {context.Exception.Message}").Build(),
                new Newtonsoft.Json.JsonSerializerSettings(), Encoding.UTF8, context.Request);
        }
    }
}
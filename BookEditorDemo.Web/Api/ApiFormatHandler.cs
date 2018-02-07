using BookEditorDemo.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace BookEditorDemo.Web.Api
{
    public class ApiResultFilterAttribute: ActionFilterAttribute
    {
        async public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (actionExecutedContext.ActionContext.ControllerContext.Controller is FileController)
            {
                return;
            }

            if (actionExecutedContext.Exception != null || !actionExecutedContext.Response.IsSuccessStatusCode)
            {
                return;
            }

            var content = actionExecutedContext.Response.Content as ObjectContent;
            if (content == null)
            {
                content = new ObjectContent<ApiResult>(null, new JsonMediaTypeFormatter());
                actionExecutedContext.Response.Content = content;
            }

            if (content.Value != null && content.Value is ApiResult)
            {
                return;
            }

            content.Value = ResultBuilder.Success().WithData(content.Value);
        }
    }
}
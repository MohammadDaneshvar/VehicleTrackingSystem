using Framework.Application;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service.AspDotNetDistributor.Filters
{
    public class EncodeInputsActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ProcessHtmlEncoding(context);
            var resultContext = await next();
        }

        private static void ProcessHtmlEncoding(ActionExecutingContext context)
        {
            context.ActionArguments.ToList().ForEach(arg => { arg.Value.ParseProperties(); });
        }
    }
}

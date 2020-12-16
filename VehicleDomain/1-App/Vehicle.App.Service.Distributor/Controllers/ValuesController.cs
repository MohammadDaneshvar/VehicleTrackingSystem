using System;
using System.Threading.Tasks;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector.Lifestyles;
using Framework.Application.Common.Extentions;
using NLog;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using App.Service.AspDotNetDistributor.Common;

namespace App.Service.AspDotNetDistributor.Controllers
{
    [GeneratedController("api2")]

    [Authorize]
    public class BaseController<T> : Controller where T : IRestrictedCommand
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly ICommandBus _commandBus;
        private readonly ICurrentUserService _currentUserService;
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public BaseController(IServiceProvider serviceProvider, ICurrentUserService currentUserServer)
        {
            this._serviceProvider = Startup._container;
            this._commandBus = _serviceProvider.GetService<ICommandBus>();
            _currentUserService = currentUserServer;

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(T command)
        {
            var result = await MakeHandlerAsync(command);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]T command)
        {
            var result = await MakeHandlerAsync(command);
            return result;
        }

        private async Task<IActionResult> MakeHandlerAsync(T command)
        {
            IActionResult result = null;
            {
                using (AsyncScopedLifestyle.BeginScope(Startup._container))
                    await _commandBus.DispatchAsync(command);
                if (command.HaveResult())
                {
                    var commandResult = command.GetResult();
                    result = Ok(commandResult);
                    var json = JsonConvert.SerializeObject(commandResult, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    _logger.Info(json);
                }
                else
                    result = Ok();
            }

            return result;

        }
    }
}

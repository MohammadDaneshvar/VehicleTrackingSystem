using Framework.Application.Common.Extentions;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using System.Globalization;
using System.Resources;

namespace Framework.Application
{
    public class LogDecoratorCommandHandler<T> : ICommandHandler<T> where T : IRestrictedCommand
    {
        private readonly ICommandHandler<T> _decorator;
        private readonly ICommandBus _commandBus;
        private readonly ICurrentUserService _currentUserService;
        
        public LogDecoratorCommandHandler(ICommandHandler<T> decorator, ICommandBus commandBus, ICurrentUserService currentUserService)
        {
            _decorator = decorator;
            _commandBus = commandBus;
            _currentUserService = currentUserService;
        }

        
        public async Task HandleAsync(T command, CancellationToken cancellationToken)
        {
            string actionId = Guid.NewGuid().ToString();

            Logger actionLogger = LogManager.GetLogger("actionFileLogger");

            await _decorator.HandleAsync(command, cancellationToken);

            string actionLog = "{";

            actionLog += string.Format("\"name\":{0},\"info\":{1},\"id\":{2},\"actorId\":{3},\"status\":{4},\"ip\":{5},\"desc\":{6}", "\"" + command.GetType().Name + "\"",
                "\"" 
                //+EnumHelper.GetDisplayName(command.ActionTypeEnum) + "\""
                ,
                "\"" + actionId + "\"",
                "\"" + _currentUserService.UserId + "\"",
                command.HaveResult() ? "\"Succeeded\"" : "\"Failed\"",
                "\"" + _currentUserService.Ip + "\"",
                "\"" + string.Empty + "\"");

            actionLog += "}";

            actionLogger.Info(actionLog);

            var type = command.GetType();
            PropertyInfo[] propertyInfo;
            propertyInfo = type.GetProperties();

            Logger detailLogger = LogManager.GetLogger("detailFileLogger");
            string detailLog = "{";

            for (int i = 0; i < propertyInfo.Length; i++)
            {
                if (propertyInfo[i].Name != "Result" && propertyInfo[i].Name != "Roles" && propertyInfo[i].Name != "CalculateRequestCost")
                {
                    detailLog += string.Format("\"id\":{0},\"name\":{1},\"info\":{2},\"type\":{3},\"value\":{4}",
                            "\"" + actionId + "\"",
                            "\"" + propertyInfo[i].Name + "\"",
                            "\"" + GetInformation(propertyInfo[i].Name) + "\"",
                            "\"" + propertyInfo[i].PropertyType.ToString() + "\"",
                            "\"" + GetPropValue(command, propertyInfo[i].Name) + "\"");

                    detailLog += "}";

                    detailLogger.Info(detailLog);

                    detailLog = "{";
                }
            }


        }

        private List<Domain.Models.Information> GetAllInformations()
        {
            List<Domain.Models.Information> Informations = new List<Domain.Models.Information>();
            ResourceManager MyResourceClass = new ResourceManager(typeof(Domain.Resource.Display));

            ResourceSet resourceSet = MyResourceClass.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

            foreach (DictionaryEntry entry in resourceSet)
            {
                Informations.Add(new Domain.Models.Information
                {
                    Name = entry.Key.ToString(),
                    Value = entry.Value.ToString()
                });
            }

            return Informations;
        }

        private string GetInformation(string name)
        {
            try
            {
                return GetAllInformations().Where(x => x.Name == name).FirstOrDefault()?.Value??"";
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }



    public static class ObjectExtensions
    {
        public static T CastTo<T>(this object o) => (T)o;

        public static dynamic CastToReflected(this object o, Type type)
        {
            var methodInfo = typeof(ObjectExtensions).GetMethod(nameof(CastTo), BindingFlags.Static | BindingFlags.Public);
            var genericArguments = new[] { type };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            return genericMethodInfo?.Invoke(null, new[] { o });
        }
    }
}

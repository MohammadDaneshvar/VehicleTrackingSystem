using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using App.Service.AspDotNetDistributor.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace App.Service.AspDotNetDistributor
{
    public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var currentAssembly = typeof(GenericTypeControllerFeatureProvider).Assembly;
            var candidates = currentAssembly.GetExportedTypes().Where(x => x.GetCustomAttributes<GeneratedControllerAttribute>().Any());

            foreach (var candidate in candidates)
            {
                var controller = typeof(BaseController<>).MakeGenericType(candidate).GetTypeInfo();
                feature.Controllers.Add(controller);
            }
        }
    }
}

//using AppService.Contracts.Attributes;
//using Framework.Application.Common.Extentions;
//using Microsoft.OpenApi.Extensions;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace App.Service.AspDotNetDistributor.Filters
//{
//    public class TagDescriptionsDocumentFilter : IDocumentFilter
//    {
//        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
//        {
//            foreach (SwaggerTag tag in Enum.GetValues((Type)typeof(SwaggerTag)))
//            {
//                OpenApiTag item = new OpenApiTag();
//                item.Name = tag.ToString();
//                item.Description = EnumHelper.GetDescription((Enum)tag);
//                swaggerDoc.Tags.Add(item);
//            }
//        }
//    }
//}

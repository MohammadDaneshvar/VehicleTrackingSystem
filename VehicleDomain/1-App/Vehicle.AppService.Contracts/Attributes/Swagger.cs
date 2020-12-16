using System;
using System.ComponentModel;
namespace Vehicle.AppService.Contracts.Attributes
{

    public class Swagger: Attribute
    {
        public SwaggerTag[] Tags { get;private  set; }
        public Swagger(SwaggerTag[] tags )
        {
            Tags = tags;
        }
    }

  public  enum SwaggerTag : byte
    {
        [Description("Everything about your User")]
        User
    }
}

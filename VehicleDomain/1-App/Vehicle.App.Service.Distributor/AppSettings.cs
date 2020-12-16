using System.Collections.Generic;

namespace App.Service.AspDotNetDistributor
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public IdentityServer IdentityServer { get; set; }
        public string ApplicationLaunchUrl { get; set; }
        
    }


    public class ConnectionStrings
    {
        public string ConnectionString { get; set; }
    }

    public class IdentityServer
    {
        public string Url { get; set; }
        public string ClientIdGrantTypeCode { get; set; }
        public string ClientIdGrantTypePassword { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
    }
  
}

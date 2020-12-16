using Framework.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;

namespace App.Service.AspDotNetDistributor
{
    public class CurrentUserService : ICurrentUserService
    {

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            IEnumerable<Claim> claims = httpContextAccessor.HttpContext?.User?.Claims;
            if (claims == null)
            {
                return;
            }

            foreach (Claim claim in claims)
            {
                switch (claim.Type)
                {
                    case "sub":
                        UserId = int.Parse(claim.Value);
                        break;
                    case "username":
                        UserName = claim.Value;
                        break;
                    
                    case "name":
                    case "email":
                    case "orgName":
                    case "Domain":

                    default:
                        var valu = claim.Value;
                        break;
                }
            }
            Ip = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        }

        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Ip { get; set; }

    }
}

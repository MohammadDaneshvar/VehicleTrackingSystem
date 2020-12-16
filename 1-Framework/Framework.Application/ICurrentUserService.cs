using System.Collections.Generic;

namespace Framework.Application
{
    public interface ICurrentUserService
    {
        int? UserId { get; set; }
        string UserName { get; set; }
        string Ip { get; set; }
    }
}

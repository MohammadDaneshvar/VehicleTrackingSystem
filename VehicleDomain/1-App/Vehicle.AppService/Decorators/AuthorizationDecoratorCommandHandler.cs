using Framework.Application;
using Framework.Domain.Enum;
using Framework.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.Domain;

namespace Vehicle.AppService.Decorators
{
    public class AuthorizationDecoratorCommandHandler<T> : ICommandHandler<T> where T : IRestrictedCommand
    {
        private readonly ICommandHandler<T> _decoratee;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly ICacheProvider _cacheProvider;
        private readonly ICurrentUserService _currentUser;

        public AuthorizationDecoratorCommandHandler(ICommandHandler<T> decoratee,
            ICurrentUserService currentUser, IRepository<RolePermission> rolePermissionRepository,
            IRepository<UserRole> userRoleRepository, ICacheProvider cacheProvider)
        {
            _decoratee = decoratee;
            this._currentUser = currentUser;
            this._rolePermissionRepository = rolePermissionRepository;
            this._userRoleRepository = userRoleRepository;
            this._cacheProvider = cacheProvider;
        }
       
        public async Task HandleAsync(T command, CancellationToken cancellationToken)
        {
            var currentCommandPermission = command.Permission;
            var currentUserPermissions = new List<CommandPermission>();
            var cacheKey = "User" + _currentUser.UserId;
            var existInCache = await _cacheProvider.ExistAsync(cacheKey);
            if (existInCache)
                currentUserPermissions = await _cacheProvider.GetAsync<List<CommandPermission>>(cacheKey);
            else
            {
                var userRole = _userRoleRepository.Where(x => x.UserId == _currentUser.UserId).Select(x => x.RoleId).ToList();
                currentUserPermissions = await _rolePermissionRepository.Where(x => userRole.Contains(x.RoleId)).Select(x => x.PermissionId).ToListAsync();
                await _cacheProvider.AddAsync(cacheKey, currentUserPermissions);
            }
            var hasAccess = currentUserPermissions.Where(x => x == command.Permission).Any();
            if (hasAccess)
                await _decoratee.HandleAsync(command, cancellationToken);
            else
            {
                throw new ExceptionResult(StatusEnum.Forbidden);
            }
        }
    }
}

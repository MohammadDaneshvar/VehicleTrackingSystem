using System.Threading;
using System.Threading.Tasks;

namespace Framework.Application
{
    public class _CacheDecoratorCommandHandler<T,TResult> : ICommandHandler<T> where T : IRestrictedCommand,IHaveResult<TResult>
    {
        private ICacheProvider cacheProvider;
        private readonly IKeyGenerator _keyGenerator;
        private readonly ICommandHandler<T> _decoratee;

        public _CacheDecoratorCommandHandler(ICacheProvider cacheProvider, IKeyGenerator keyGenerator, ICommandHandler<T> decoratee)
        {
            this.cacheProvider = cacheProvider;
            _keyGenerator = keyGenerator;
            _decoratee = decoratee;
        }
        public async Task HandleAsync(T command, CancellationToken cancellationToken)
        {
           
            var key = _keyGenerator.GenerateKeyForCache(command);
            if (string.IsNullOrEmpty(key))
            {
                await _decoratee.HandleAsync(command, cancellationToken);
            }
            else
            {
                var exist = await cacheProvider.ExistAsync(key);
                if (exist)
                {
                    var result = await cacheProvider.GetAsync<TResult>(key);
                    if (result != null)
                        command.Result = result;
                }
                else
                {
                    await _decoratee.HandleAsync(command, cancellationToken);
                    await cacheProvider.AddAsync(key, command.Result);

                }
            }
        }
    }
}
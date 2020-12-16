using Framework.Data.EF;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Application
{
    public class TransactionalCommandHandler<T> : ICommandHandler<T>
    {
        private readonly ICommandHandler<T> _decoratee;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionalCommandHandler(ICommandHandler<T> decoratee, IUnitOfWork unitOfWork)
        {
            _decoratee = decoratee;
            _unitOfWork = unitOfWork;
        }
        public async Task HandleAsync(T command, CancellationToken cancellationToken)
        {
                await _decoratee.HandleAsync(command, cancellationToken);
                await _unitOfWork.SaveChangesAsync();
        }
    }
}
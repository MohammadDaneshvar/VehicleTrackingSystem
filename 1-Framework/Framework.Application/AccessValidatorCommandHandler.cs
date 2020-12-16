using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Application
{
    public class AccessValidatorCommandHandler<T> : ICommandHandler<T> where T : IRestrictedCommand
    {
        private readonly ICommandHandler<T> _decoratee;
        private readonly ICommandValidator<T> validator;

        public AccessValidatorCommandHandler(ICommandHandler<T> decoratee, ICommandValidator<T> validator)
        {
            _decoratee = decoratee;
            this.validator = validator;
        }
        public async Task HandleAsync(T command, CancellationToken cancellationToken)
        {
            var results= validator.Validate(command);
            if (results.IsValid)
                await _decoratee.HandleAsync(command, cancellationToken);
            else
            {
                var failures= results.Errors.Where(f => f != null).ToList();
                throw new ValidationException(failures);
            }
        }
    }
}
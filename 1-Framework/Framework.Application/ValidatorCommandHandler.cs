using FluentValidation;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Application
{
    public class ValidatorCommandHandler<T> : ICommandHandler<T>
    {
        private readonly ICommandHandler<T> _decoratee;
        private readonly ICommandValidator<T> _commandValidator;

        public ValidatorCommandHandler(ICommandHandler<T> decoratee, ICommandValidator<T> validator)
        {
            _decoratee = decoratee;
            this._commandValidator = validator;
        }
        public async Task HandleAsync(T command, CancellationToken cancellationToken)
        {
           var results=   _commandValidator.Validate(command);
            if (!results.IsValid)
            {
                var failures = results.Errors.Where(f => f != null).ToList();
                throw new ValidationException(failures);
            }
            await _decoratee.HandleAsync(command, cancellationToken);
        }
    }
}
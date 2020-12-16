

using FluentValidation.Results;
using System.Threading.Tasks;

namespace Framework.Application
{
    public interface ICommandValidator<T>
    {
        ValidationResult Validate(T command);
    }
}
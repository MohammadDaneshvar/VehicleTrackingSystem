using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Application
{
    public interface ICommandHandler<in T>
    {
        Task HandleAsync(T command, CancellationToken cancellationToken);
    }
}

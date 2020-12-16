using System.ServiceModel;
using System.Threading.Tasks;

namespace Framework.Application
{
    [ServiceContract]
    public interface ICommandHandlerService<TCommand>
    {
        [OperationContract]
        Task<string> SendAsync(TCommand command);
    }
}
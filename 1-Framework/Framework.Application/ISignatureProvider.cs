using System;
using System.Threading.Tasks;

namespace Framework.Application
{
    public interface ISignatureProvider
    {
        bool ValidateSignature<T>(T command,string publicKey, string digitalSignature) where T: IRestrictedCommand;
    }
}
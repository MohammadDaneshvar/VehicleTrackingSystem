using Framework.Data.EF;
using Framework.Domain.Enum;
using Framework.Domain.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Application
{
    public class SignatureServiceProvider : ISignatureProvider
    {
        private readonly IKeyGenerator _keyGenerator;

        public SignatureServiceProvider(IKeyGenerator keyGenerator)
        {
            _keyGenerator = keyGenerator;
        }
        public bool ValidateSignature<T>(T command, string publicKey,string digitalSignature) where T :IRestrictedCommand
        {
            var key=  Encoding.ASCII.GetBytes(_keyGenerator.GenerateKeyForPKI(command));
            var verifier = new DSACryptoServiceProvider();
            var hashByte = Encoding.ASCII.GetBytes(digitalSignature);
            var isValid=  verifier.VerifyData(key, hashByte);
            return isValid;
        }
    }
}

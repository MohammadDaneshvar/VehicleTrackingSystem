using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Application
{
    //public class CacheDecoratorCommandHandler<T, TResult> : ICommandHandler<T> where T : IRestrictedCommand, IHaveResult<TResult>
    //{
    //    private readonly IKeyGenerator _keyGenerator;
    //    private readonly ICommandHandler<T> _decorator;
    //    private readonly IRequestCacheService _request;

    //    public CacheDecoratorCommandHandler(ICommandHandler<T> decorator, IRequestCacheService request, IKeyGenerator keyGenerator)
    //    {
    //        _decorator = decorator;
    //        _request = request;
    //        _keyGenerator = keyGenerator;
    //    }

    //    public static T GetValue<T>(String value)
    //    {
    //        return (T)Convert.ChangeType(value, typeof(T));
    //    }

    //    private JObject Settings()
    //    {
    //        JObject jObject = JObject.Parse(File.ReadAllText(@"appsettings.json"));
    //        return jObject;
    //    }

    //    public async Task HandleAsync(T command, CancellationToken cancellationToken)
    //    {
    //        //var enabled = ((JValue)((JProperty)((JContainer)Settings()["CacheSettings"]).First).Value).Value;
                        
    //        var key = _keyGenerator.GenerateKeyForCache(command);
    //        if (string.IsNullOrEmpty(key))
    //        {
    //            await _decorator.HandleAsync(command, cancellationToken);
    //        }
    //        else
    //        {
    //            var cachedRequest = await _request.GetAsync(key);

    //            if (!string.IsNullOrWhiteSpace(cachedRequest))
    //            {
    //                command.Result = JsonConvert.DeserializeObject<TResult>(cachedRequest);
    //            }
    //            else
    //            {
    //                var time = ((JValue)((JProperty)((JContainer)Settings()["CacheSettings"]).Last).Value).Value;
    //                await _decorator.HandleAsync(command, cancellationToken);
    //                await _request.AddAsync(key, command.Result, TimeSpan.FromSeconds(Convert.ToDouble(time)));
    //            }

    //        }
    //    }
    //}
}

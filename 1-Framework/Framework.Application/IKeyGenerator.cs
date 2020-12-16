namespace Framework.Application
{
    public interface IKeyGenerator
    {
        string GenerateKeyForCache(IRestrictedCommand command);
        string GenerateKeyForPKI(IRestrictedCommand command);
    }
}
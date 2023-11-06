namespace Swapy.BLL.Interfaces
{
    public interface IKeyVaultService
    {
        Task<string> GetSecretValue(string secretName);
    }
}

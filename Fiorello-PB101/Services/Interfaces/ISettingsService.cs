namespace Fiorello_PB101.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<Dictionary<string, string>> GetAllAsync();
    }
}

using Fiorello_PB101.Data;
using Fiorello_PB101.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_PB101.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly AppDbContext _context;
        public SettingsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string, string>> GetAllAsync()
        {
            return await _context.Settings.ToDictionaryAsync(m=>m.Key, m=>m.Value);
        }
    }
}

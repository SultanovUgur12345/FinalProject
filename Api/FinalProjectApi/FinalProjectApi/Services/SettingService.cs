using FinalProjectApi.Data;
using FinalProjectApi.DTOs.Setting;
using FinalProjectApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApi.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SettingGetDto>> GetAllAsync()
        {
            return await _context.Settings
                .Select(s => new SettingGetDto { Id = s.Id, Key = s.Key, Value = s.Value })
                .ToListAsync();
        }

        public async Task<SettingGetDto> GetByIdAsync(int id)
        {
            var s = await _context.Settings.FindAsync(id);
            if (s == null) return null;
            return new SettingGetDto { Id = s.Id, Key = s.Key, Value = s.Value };
        }

        public async Task UpdateAsync(int id, SettingUpdateDto dto)
        {
            var s = await _context.Settings.FindAsync(id);
            if (s == null) throw new Exception("Setting not found");
            s.Value = dto.Value;
            await _context.SaveChangesAsync();
        }
    }
}

using AutoMapper;
using FinalProjectApi.Data;
using FinalProjectApi.DTOs.AboutUs;
using FinalProjectApi.Models;
using FinalProjectApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApi.Services
{
    public class AboutUsService : IAboutUsService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public AboutUsService(AppDbContext context, IMapper mapper, IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<AboutUsGetDto>> GetAllAsync()
        {
            var items = await _context.AboutUs
                .Include(a => a.Achievements)
                .OrderByDescending(a => a.CreateDate)
                .ToListAsync();
            return _mapper.Map<List<AboutUsGetDto>>(items);
        }

        public async Task<List<AboutUsGetDto>> GetAllAdminAsync()
        {
            var items = await _context.AboutUs
                .Include(a => a.Achievements)
                .OrderByDescending(a => a.CreateDate)
                .ToListAsync();
            return _mapper.Map<List<AboutUsGetDto>>(items);
        }

        public async Task<AboutUsGetDto> GetByIdAsync(int id)
        {
            var item = await _context.AboutUs
                .Include(a => a.Achievements)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (item == null) return null;
            return _mapper.Map<AboutUsGetDto>(item);
        }

        public async Task CreateAsync(AboutUsCreateDto dto)
        {
            var aboutUs = _mapper.Map<AboutUs>(dto);

            if (dto.Image != null)
            {
                string fileName = await _fileService.UploadAsync(dto.Image, "aboutus");
                aboutUs.Image = fileName;
            }

            await _context.AboutUs.AddAsync(aboutUs);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, AboutUsUpdateDto dto)
        {
            var aboutUs = await _context.AboutUs
                .Include(a => a.Achievements)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aboutUs == null)
                throw new Exception("AboutUs tapilmadi");

            aboutUs.SubTitle = dto.SubTitle;
            aboutUs.Title = dto.Title;
            aboutUs.Description = dto.Description;
            aboutUs.BottomText = dto.BottomText;
            aboutUs.VideoUrl = dto.VideoUrl;

            if (dto.Image != null)
            {
                string fileName = await _fileService.UploadAsync(dto.Image, "aboutus");
                aboutUs.Image = fileName;
            }
            else
            {
                aboutUs.Image = dto.CurrentImage;
            }

            _context.AboutUsAchievements.RemoveRange(aboutUs.Achievements);
            aboutUs.Achievements = _mapper.Map<List<AboutUsAchievement>>(dto.Achievements);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var aboutUs = await _context.AboutUs
                .Include(a => a.Achievements)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (aboutUs == null) return;

            _context.AboutUsAchievements.RemoveRange(aboutUs.Achievements);
            _context.AboutUs.Remove(aboutUs);
            await _context.SaveChangesAsync();
        }
    }
}

using AutoMapper;
using FinalProjectApi.Data;
using FinalProjectApi.DTOs.Feature;
using FinalProjectApi.Models;
using FinalProjectApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApi.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public FeatureService(AppDbContext context, IMapper mapper, IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<FeatureGetDto>> GetAllAsync()
        {
            var features = await _context.Features.OrderByDescending(f => f.CreateDate).ToListAsync();
            return _mapper.Map<List<FeatureGetDto>>(features);
        }

        public async Task<FeatureGetDto> GetByIdAsync(int id)
        {
            var feature = await _context.Features.FindAsync(id);
            if (feature == null) return null;
            return _mapper.Map<FeatureGetDto>(feature);
        }

        public async Task CreateAsync(FeatureCreateDto dto)
        {
            var feature = _mapper.Map<Feature>(dto);

            if (dto.Image != null)
            {
                string fileName = await _fileService.UploadAsync(dto.Image, "features");
                feature.Image = fileName;
            }

            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, FeatureUpdateDto dto)
        {
            var feature = await _context.Features.FindAsync(id);
            if (feature == null)
                throw new Exception("Feature tapilmadi");

            _mapper.Map(dto, feature);

            if (dto.Image != null)
            {
                string fileName = await _fileService.UploadAsync(dto.Image, "features");
                feature.Image = fileName;
            }
            else
            {
                feature.Image = dto.CurrentImage;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var feature = await _context.Features.FindAsync(id);
            if (feature == null) return;

            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();
        }
    }
}

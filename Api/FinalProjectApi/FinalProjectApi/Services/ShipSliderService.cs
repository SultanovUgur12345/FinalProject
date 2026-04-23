using AutoMapper;
using FinalProjectApi.Data;
using FinalProjectApi.DTOs.ShipSlider;
using FinalProjectApi.Models;
using FinalProjectApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApi.Services
{
    public class ShipSliderService : IShipSliderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ShipSliderService(AppDbContext context, IMapper mapper, IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<ShipSliderGetDto>> GetAllAsync()
        {
            var sliders = await _context.ShipSliders.OrderByDescending(s => s.CreateDate).ToListAsync();
            return _mapper.Map<List<ShipSliderGetDto>>(sliders);
        }

        public async Task<List<ShipSliderGetDto>> GetAllAdminAsync()
        {
            var sliders = await _context.ShipSliders.OrderByDescending(s => s.CreateDate).ToListAsync();
            return _mapper.Map<List<ShipSliderGetDto>>(sliders);
        }

        public async Task<ShipSliderGetDto> GetByIdAsync(int id)
        {
            var slider = await _context.ShipSliders.FindAsync(id);
            if (slider == null) return null;

            return _mapper.Map<ShipSliderGetDto>(slider);
        }

        public async Task CreateAsync(ShipSliderCreateDto dto)
        {
            var slider = _mapper.Map<ShipSlider>(dto);

            if (dto.Image != null)
            {
                string fileName = await _fileService.UploadAsync(dto.Image, "shipsliders");
                slider.Image = fileName;
            }

            await _context.ShipSliders.AddAsync(slider);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ShipSliderUpdateDto dto)
        {
            var slider = await _context.ShipSliders.FindAsync(id);
            if (slider == null)
                throw new Exception("ShipSlider tapilmadi");

            _mapper.Map(dto, slider);

            if (dto.Image != null)
            {
                string fileName = await _fileService.UploadAsync(dto.Image, "shipsliders");
                slider.Image = fileName;
            }
            else
            {
                slider.Image = dto.CurrentImage;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var slider = await _context.ShipSliders.FindAsync(id);
            if (slider == null) return;

            _context.ShipSliders.Remove(slider);
            await _context.SaveChangesAsync();
        }
    }
}

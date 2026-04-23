using AutoMapper;
using FinalProjectApi.Data;
using FinalProjectApi.DTOs.Partner;
using FinalProjectApi.Models;
using FinalProjectApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApi.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public PartnerService(AppDbContext context, IMapper mapper, IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<PartnerGetDto>> GetAllAsync()
        {
            var partners = await _context.Partners.OrderByDescending(p => p.CreateDate).ToListAsync();
            return _mapper.Map<List<PartnerGetDto>>(partners);
        }

        public async Task<PartnerGetDto> GetByIdAsync(int id)
        {
            var partner = await _context.Partners.FindAsync(id);
            if (partner == null) return null;
            return _mapper.Map<PartnerGetDto>(partner);
        }

        public async Task CreateAsync(PartnerCreateDto dto)
        {
            var partner = new Partner();

            if (dto.Image != null)
            {
                string fileName = await _fileService.UploadAsync(dto.Image, "partners");
                partner.Image = fileName;
            }

            await _context.Partners.AddAsync(partner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, PartnerUpdateDto dto)
        {
            var partner = await _context.Partners.FindAsync(id);
            if (partner == null)
                throw new Exception("Partner tapilmadi");

            if (dto.Image != null)
            {
                string fileName = await _fileService.UploadAsync(dto.Image, "partners");
                partner.Image = fileName;
            }
            else
            {
                partner.Image = dto.CurrentImage;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var partner = await _context.Partners.FindAsync(id);
            if (partner == null) return;

            _context.Partners.Remove(partner);
            await _context.SaveChangesAsync();
        }
    }
}

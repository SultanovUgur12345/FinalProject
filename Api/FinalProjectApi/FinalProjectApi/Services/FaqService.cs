using AutoMapper;
using FinalProjectApi.Data;
using FinalProjectApi.DTOs.Faq;
using FinalProjectApi.Models;
using FinalProjectApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApi.Services
{
    public class FaqService : IFaqService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FaqService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FaqGetDto>> GetAllAsync()
        {
            var faqs = await _context.Faqs.OrderByDescending(f => f.CreateDate).ToListAsync();
            return _mapper.Map<List<FaqGetDto>>(faqs);
        }

        public async Task<FaqGetDto> GetByIdAsync(int id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            if (faq == null) return null;
            return _mapper.Map<FaqGetDto>(faq);
        }

        public async Task CreateAsync(FaqCreateDto dto)
        {
            var faq = _mapper.Map<Faq>(dto);
            await _context.Faqs.AddAsync(faq);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, FaqUpdateDto dto)
        {
            var faq = await _context.Faqs.FindAsync(id);
            if (faq == null)
                throw new Exception("Faq tapilmadi");

            _mapper.Map(dto, faq);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            if (faq == null) return;

            _context.Faqs.Remove(faq);
            await _context.SaveChangesAsync();
        }
    }
}

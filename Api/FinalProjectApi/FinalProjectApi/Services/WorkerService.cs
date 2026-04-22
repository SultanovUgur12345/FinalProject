using AutoMapper;
using FinalProjectApi.Data;
using FinalProjectApi.DTOs.Worker;
using FinalProjectApi.Models;
using FinalProjectApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApi.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;


        public WorkerService(AppDbContext context, IMapper mapper, IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<WorkerGetDto>> GetAllAsync()
        {
            var workers = await _context.Workers.OrderByDescending(m=>m.CreateDate).ToListAsync();
            return _mapper.Map<List<WorkerGetDto>>(workers);
        }

        public async Task<List<WorkerGetDto>> SearchByNameAsync(string name)
        {
            var workers = await _context.Workers
                .Where(w => w.FullName.ToLower().Contains(name.ToLower()))
                .OrderByDescending(w => w.CreateDate)
                .ToListAsync();

            return _mapper.Map<List<WorkerGetDto>>(workers);
        }

        public async Task<WorkerDetailDto> GetByIdAsync(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null) return null;

            return _mapper.Map<WorkerDetailDto>(worker);
        }

        public async Task CreateAsync(WorkerCreateDto dto)
        {
            var worker = _mapper.Map<Worker>(dto);

            if (dto.Image != null)
            {
                string fileName = await _fileService.UploadAsync(dto.Image, "workers");
                worker.Image = fileName;
            }

            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, WorkerUpdateDto dto)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
                throw new Exception("Worker tapilmadi");

            _mapper.Map(dto, worker);

            if (dto.Image != null)
            {
                string fileName = await _fileService.UploadAsync(dto.Image, "workers");
                worker.Image = fileName;
            }
            else
            {
                worker.Image = dto.CurrentImage;
            }

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null) return;

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
        }
    }
}

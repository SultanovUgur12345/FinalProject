using FinalProjectApi.DTOs.Worker;

namespace FinalProjectApi.Services.Interfaces
{
    public interface IWorkerService
    {
        Task<List<WorkerGetDto>> GetAllAsync();
        Task<List<WorkerGetDto>> SearchByNameAsync(string name);
        Task<WorkerDetailDto> GetByIdAsync(int id);

        Task CreateAsync(WorkerCreateDto dto);
        Task UpdateAsync(int id, WorkerUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

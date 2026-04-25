using FinalProjectMVC.ViewModels.Worker;

namespace FinalProjectMVC.Services.Interfaces
{
    public interface IWorkerApiService
    {
        Task<List<WorkerGetVM>> GetAllAsync();
        Task<PaginatedResult<WorkerGetVM>> GetPagedAsync(int page, int pageSize);
        Task<List<WorkerGetVM>> SearchByNameAsync(string name);
        Task<WorkerDetailVM> GetByIdAsync(int id);
        Task<WorkerEditVM> GetByIdForEditAsync(int id);
        Task CreateAsync(WorkerCreateVM dto);
        Task UpdateAsync(int id, WorkerEditVM dto);
        Task DeleteAsync(int id);
    }
}

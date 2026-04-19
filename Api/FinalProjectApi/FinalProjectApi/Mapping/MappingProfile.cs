using AutoMapper;
using FinalProjectApi.DTOs.Account;
using FinalProjectApi.DTOs.Worker;
using FinalProjectApi.Models;

namespace FinalProjectApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WorkerCreateDto, Worker>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<WorkerUpdateDto, Worker>()
              .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Worker, WorkerGetDto>();

            CreateMap<WorkerUpdateDto, Worker>();

            CreateMap<Worker, WorkerDetailDto>();

            CreateMap<RegisterDto, AppUser>();
            CreateMap<UpdateProfileDto, AppUser>()
                .ForMember(x => x.Id, opt => opt.Ignore());

        }
    }
}

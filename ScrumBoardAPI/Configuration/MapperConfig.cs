using AutoMapper;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Models.Task;
using ScrumBoardAPI.Models.User;
using ScrumBoardAPI.Models.Workspace;

namespace ScrumBoardAPI.Configuration;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Workspace, GetWorkspaceDto>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name));

        CreateMap<Workspace, GetWorkspaceDetailsDto>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("tasks", opt => opt.MapFrom(src => src.Tasks))
            .ForCtorParam("users", opt => opt.MapFrom(src => src.Users))
            .ForCtorParam("admin", opt => opt.MapFrom(src => src.Admin));

        CreateMap<CreateUserDto, AUser>();

        CreateMap<AUser, GetUserDto>()
            .ForCtorParam("userId", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("userName", opt => opt.MapFrom(src => src.UserName))
            .ForCtorParam("email", opt => opt.MapFrom(src => src.Email));

        CreateMap<ATask, GetTaskDto>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("description", opt => opt.MapFrom(src => src.Description))
            .ForCtorParam("priority", opt => opt.MapFrom(src => src.Priority))
            .ForCtorParam("workspaceId", opt => opt.MapFrom(src => src.WorkspaceId))
            .ForCtorParam("status", opt => opt.MapFrom(src => src.Status))
            .ForCtorParam("creatorId", opt => opt.MapFrom(src => src.CreatorId));

        CreateMap<CreateTaskDto, ATask>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("description", opt => opt.MapFrom(src => src.Description))
            .ForCtorParam("priority", opt => opt.MapFrom(src => src.Priority))
            .ForCtorParam("workspaceId", opt => opt.MapFrom(src => src.WorkspaceId))
            .ForCtorParam("status", opt => opt.MapFrom(src => src.Status))
            .ForCtorParam("creatorId", opt => opt.MapFrom(src => src.CreatorId));

        CreateMap<PutTaskDto, ATask>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

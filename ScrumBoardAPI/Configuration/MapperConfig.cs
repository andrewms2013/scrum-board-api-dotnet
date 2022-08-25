using AutoMapper;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Models.User;
using ScrumBoardAPI.Models.Workspace;

namespace ScrumBoardAPI.Configuration;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Workspace, CreateWorkspaceDto>().ReverseMap();
        CreateMap<Workspace, GetWorkspaceDto>().ReverseMap();
        CreateMap<Workspace, UpdateWorkspaceDto>().ReverseMap();

        CreateMap<CreateUserDto, AUser>().ReverseMap();
    }
}

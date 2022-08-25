using AutoMapper;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Models.User;

namespace ScrumBoardAPI.Configuration;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<AUser, CreateUserDto>().ReverseMap();
        CreateMap<AUser, GetUserDto>().ReverseMap();
        CreateMap<AUser, UpdateUserDto>().ReverseMap();
    }
}

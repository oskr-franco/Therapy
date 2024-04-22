using AutoMapper;
using Therapy.Core.Utils;
using Therapy.Domain.DTOs.User;
using Therapy.Domain.Entities;

namespace Therapy.Core.Mappings {
  public class UserMappingProfile : Profile {
    public UserMappingProfile() {
      CreateMap<UserCreateDTO, User>()
        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Hasher.Hash(src.Password)));
      CreateMap<User, UserDTO>();
      CreateMap<UserUpdateDTO, User>()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
  }
}
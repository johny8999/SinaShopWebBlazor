using AutoMapper;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;
using SinaShop.Application.Contract.PresentationDTO.ViewInputs;

namespace SinaShop.Application.Contract.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<viLogIn, InpLoginByEmailPassword>();
            CreateMap<viForgetPassword, InpForgetPassword>();
        }
    }
}

using AutoMapper;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;
using SinaShop.Application.Contract.JwtDTO;
using SinaShop.Application.Contract.PresentationDTO.ViewInputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Application.Contract.Mappings
{
    public class JwtProfile: Profile
    {
        public JwtProfile()
        {
            CreateMap<InpCreateToken, InpGetAllDetails>();
        }
    }
}

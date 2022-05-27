using AutoMapper;
using FrameWork.Application.Arguments;
using FrameWork.Consts;
using FrameWork.ExMethods;
using FrameWork.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using SinaShop.Application.Contract.ApplicationDTO.Role;
using SinaShop.Application.Contract.ApplicationDTO.UsersDto;
using SinaShop.Application.Contract.JwtDTO;
using SinaShop.Application.Roles;
using SinaShop.Application.UserAplication;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SinaShop.Infrastructure.EfCore.Identity.JWT.JwtBuild
{
    public class JwtBuilder : IJwtBuilder
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IUserApplication _UserApplication;
        private readonly IRoleApplication _RoleApplication;
        private readonly IMapper _Mapper;

        public JwtBuilder(IUserApplication userApplication, IRoleApplication roleApplication,
            IServiceProvider serviceProvider, ILogger logger, IMapper mapper)
        {
            _UserApplication = userApplication;
            _RoleApplication = roleApplication;
            _ServiceProvider = serviceProvider;
            _Logger = logger;
            _Mapper = mapper;
        }

        public async Task<string> CreateTokenAsync(InpCreateToken input)
        {
            try
            {
                #region Validation
                {
                    input.CheckModelState(_ServiceProvider);
                }
                #endregion Validation

                #region  Get User
                OutGetAllDetails qUser = null;
                {
                    var MapperData = _Mapper.Map<InpGetAllDetails>(input);
                    qUser = await _UserApplication.GetAllDetailsAsync(MapperData);

                }
                #endregion  Get User

                #region Get Role
                IList<string> qRoles = null;
                {
                    var MapperData = _Mapper.Map<InpGetRolesByUser>(input);
                    qRoles = await _RoleApplication.GetRolesByUserAsync(MapperData);
                }
                #endregion Get Role

                #region Claim list
                List<Claim> Claims = new();
                {
                    Claims.AddRange(new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,qUser.Id),
                        new Claim(ClaimTypes.GivenName,qUser.FullName),
                        new Claim(ClaimTypes.Name,qUser.Email),
                        new Claim(ClaimTypes.Email,qUser.Email),
                        new Claim(ClaimTypes.MobilePhone,qUser.PhoneNumber??""),
                        new Claim("AccessLevel",qUser.AccessLevelTitle),
                        new Claim("Date",DateTime.Now.ToString("yyyy/mm/dd",new CultureInfo("en-us")))
                    });
                    Claims.AddRange(qRoles.Select(qRole => new Claim(ClaimsIdentity.DefaultRoleClaimType, qRole)));
                }
                #endregion Claim list

                #region Descriptor
                SecurityTokenDescriptor TokenDescriptor = null;
                {
                    var Key = Encoding.ASCII.GetBytes(AuthConst.SecretCode);
                    TokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(Claims),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature),
                        Audience = AuthConst.Audience,
                        Expires = DateTime.Now.AddDays(2),
                        Issuer = AuthConst.Issuer,
                        IssuedAt = DateTime.Now //az key faal beshe

                    };
                }
                #endregion Descriptor

                #region Generate token
                string GeneratedToken = null;
                {
                    var SecurityToken = new JwtSecurityTokenHandler().CreateToken(TokenDescriptor);
                    GeneratedToken = "Bearer " + new JwtSecurityTokenHandler().WriteToken(SecurityToken);
                }
                #endregion Generate token
                return GeneratedToken.AesEncpypt(AuthConst.SecretKey);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}

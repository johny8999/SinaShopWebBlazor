﻿using FrameWork.Common.ExMethods;
using FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SinaShop.Application.AccessLevel;
using SinaShop.Application.Contract.ApplicationDTO.AccessLevel;
using SinaShop.Domain.Users.UserAgg.Contracts;
using SinaShop.Domain.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Infrastructure.Seed.Base.User
{
    public class Seed_User: ISeed_User
    {
        private readonly IUserRepository _UserRepository;
        private readonly ILogger _Logger;
        private readonly IAccessLevelApplication _AccessLevelApplication;

        public Seed_User(IUserRepository userRepository, ILogger logger, IAccessLevelApplication accessLevelApplication)
        {
            _UserRepository = userRepository;
            _Logger = logger;
            _AccessLevelApplication = accessLevelApplication;
        }
        public async Task<bool> RunAsync()
        {
            try
            {
                if (!await _UserRepository.GetNoTraking.AnyAsync(a=>a.Email=="sinaalipour89@gmail.com"))
                {
                    await _UserRepository.AddAsync(new tblUsers
                    {
                        Id = "720583c2-2e7b-4ab1-a79f-08da06b48e2b".ToGuid(),
                        Email = "sinaalipour89@gmail.com",
                        NormalizedEmail = "sinaalipour89@gmail.com".ToUpper(),
                        Fullname = "سینا عالیپور",
                        Date = DateTime.Now,
                        IsActive = true,
                        EmailConfirmed=true,
                        UserName = "sinaalipour89@gmail.com",
                        NormalizedUserName = "sinaalipour8999".ToUpper(),
                        PasswordHash = "AQAAAAEAACcQAAAAEGXT98P0X0495qWpYE1/vEpMA1ZgkDxQ77iUENoFOBS93TEDMwcOhFNVUlAMQcTfEw==",
                        SecurityStamp = "DVF26VWYYHBBJSI5F3BFKQWPUCWVYLO6",
                        ConcurrencyStamp = "001061fb-c46b-43d4-9198-5e5557188802",
                        PhoneNumber="09133802351",
                        PhoneNumberConfirmed = true,
                        AccessLevelId = (await _AccessLevelApplication.GetIdByNameAsync
                            (new InpGetByIdName() { Name = "NotConfirmedUser" })).ToGuid(),
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }
    }
}

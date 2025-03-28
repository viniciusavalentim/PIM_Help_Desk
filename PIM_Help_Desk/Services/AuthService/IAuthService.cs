﻿using PIM_Help_Desk.Dtos;
using PIM_Help_Desk.Models;

namespace PIM_Help_Desk.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<AuthUserDto>> Register(RegisterDto request);
        Task<ServiceResponse<AuthUserDto>> Login(LoginDto request);
    }
}
﻿using Dtos.AlphaBank;

namespace Interfaces.Security {
    public interface IUserService {
        public Task<(bool, UserAuthenticationDto?)> Login(UserLoginDto oUserLoginDto);
        public Task<(bool, UserAuthenticationDto?)> ValidateUserId(int id);
        public Task<bool> ValidateUserPassword(int id, string password);
    }
}
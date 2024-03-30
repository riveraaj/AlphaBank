﻿using Dtos.AlphaBank.Security;
using Interfaces.Security.Repositories;

namespace Interfaces.Security.Services {
    public interface IUserService {

        public Task<bool> UserSetup(CreateEmployeeDTO oCreateEmployeeDTO, IEmployeeRepository oEmployeeRepository);

        public Task<bool> Create(CreateUserDTO oCreateUserDTO);
    }
}
﻿using Data.AlphaBank;
using Dtos.AlphaBank.Security;

namespace Mapper.Security {
    public static class RoleMapper {

        public static Role MapRole(CreateRoleDto oCreateRoleDto)
            => new() {
                Description = oCreateRoleDto.Description
            };
    }
}
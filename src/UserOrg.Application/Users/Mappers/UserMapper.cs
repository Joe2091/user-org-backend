using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOrg.BusinessLogic.Users.Dtos;
using UserOrg.Domain.Entities;

namespace UserOrg.BusinessLogic.Users.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Role,
                user.ManagerId
            );
        }
    }
}

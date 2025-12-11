using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOrg.BusinessLogic.Users.Queries;
using UserOrg.Domain.Entities;

namespace UserOrg.BusinessLogic.Users.Builders
{
    public class UserBuilder
    {
        private User userRequest;
        public UserBuilder()
        {
            userRequest = new User();
        }
        public UserBuilder WithName(string firstName, string lastName)
        {
            this.userRequest.FirstName = firstName;
            this.userRequest.LastName = lastName;
            return this;
        }

        public UserBuilder WithEmail(string emailAddress)
        {
            this.userRequest.Email = emailAddress;
            return this;
        }

        public UserBuilder WithRole(string role)
        {
            this.userRequest.Role = role;
            return this;
        }

        public UserBuilder WithManagerId(int? managerId)
        {
            this.userRequest.ManagerId = managerId;
            return this;
        }

        public User Build()
        {
            this.userRequest.CreatedAt = DateTime.UtcNow;
            this.userRequest.UpdatedAt = DateTime.UtcNow;
            return this.userRequest;
        }



    }
}

using MediatR;
using UserOrg.BusinessLogic.Users.Dtos;

namespace UserOrg.BusinessLogic.Users.Commands;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Role,
    int? ManagerId
) : IRequest<UserDto>;

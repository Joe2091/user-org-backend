using MediatR;

namespace UserOrg.BusinessLogic.Users.Commands;

public record UpdateUserCommand(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    int? ManagerId
) : IRequest<Unit>;

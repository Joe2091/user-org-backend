using MediatR;

namespace UserOrg.BusinessLogic.Users.Commands;

public record DeleteUserCommand(int Id) : IRequest<Unit>;

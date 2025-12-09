using MediatR;
using UserOrg.BusinessLogic.Users.Dtos;

namespace UserOrg.BusinessLogic.Users.Queries;

public record GetUserByIdQuery(int Id) : IRequest<UserDto?>;

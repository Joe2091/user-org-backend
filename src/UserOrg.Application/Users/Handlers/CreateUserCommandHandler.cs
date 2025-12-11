using MediatR;
using UserOrg.BusinessLogic.Interfaces;
using UserOrg.BusinessLogic.Users.Builders;
using UserOrg.BusinessLogic.Users.Commands;
using UserOrg.BusinessLogic.Users.Dtos;
using UserOrg.BusinessLogic.Users.Mappers;
using UserOrg.Domain.Entities;

namespace UserOrg.BusinessLogic.Users.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new UserBuilder()
            .WithName(request.FirstName, request.LastName)
            .WithEmail(request.Email)
            .WithRole(request.Role)
            .WithManagerId(request.ManagerId)
            .Build();

        await _userRepository.AddAsync(user);

   return user.ToDto();
    }
}

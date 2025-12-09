using MediatR;
using UserOrg.BusinessLogic.Interfaces;
using UserOrg.BusinessLogic.Users.Dtos;
using UserOrg.BusinessLogic.Users.Queries;

namespace UserOrg.BusinessLogic.Users.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user is null) return null;

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

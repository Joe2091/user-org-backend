using MediatR;
using UserOrg.BusinessLogic.Interfaces;
using UserOrg.BusinessLogic.Users.Dtos;
using UserOrg.BusinessLogic.Users.Queries;

namespace UserOrg.BusinessLogic.Users.Handlers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        return users
            .Select(u => new UserDto(
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email,
                u.Role,
                u.ManagerId))
            .ToList();
    }
}

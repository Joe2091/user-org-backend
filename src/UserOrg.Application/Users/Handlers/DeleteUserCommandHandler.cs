using MediatR;
using UserOrg.BusinessLogic.Interfaces;
using UserOrg.BusinessLogic.Users.Commands;

namespace UserOrg.BusinessLogic.Users.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>

{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user is null)
        {
            return Unit.Value;
        }

        await _userRepository.DeleteAsync(user);

        return Unit.Value;
    }

}

namespace UserOrg.BusinessLogic.Users.Dtos;

public record UserDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    int? ManagerId
);

using Core.Domain.ComplexTypes;

namespace Application.Features.Users.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public RecordStatu Status { get; set; }
}
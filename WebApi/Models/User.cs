namespace WebApi.Models;

public record User
{
    public required int Id { get; init; }
    public required string UserName { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public required string DeviceToken { get; init; }
}

using RoyalParking.Core.Interfaces;

namespace RoyalParking.Core.DTO;

public class UserDTO : IReturnable
{
    public required int Id { get; init; }
    public required string Username { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required DateTime CreateDate { get; init; }
    public DateTime? UpdateDate { get; init; }
    public int? CustomerId { get; init; }
    public int? ValetId { get; init; }
}

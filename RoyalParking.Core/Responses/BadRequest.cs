using RoyalParking.Core.Interfaces;

namespace RoyalParking.Core.Responses;

public class BadRequest : IReturnable, IResponse
{
    public required string Message { get; init; }
}

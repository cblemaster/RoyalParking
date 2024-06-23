using RoyalParking.Core.Interfaces;

namespace RoyalParking.Core.Responses;

public class Unauthorized : IReturnable, IResponse
{
    public required string Message { get; init; }
}

using RoyalParking.Core.Interfaces;

namespace RoyalParking.Core.Responses;

public class NotFound : IReturnable, IResponse
{
    public required string Message { get; init; }
}

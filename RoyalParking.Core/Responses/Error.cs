using RoyalParking.Core.Interfaces;

namespace RoyalParking.Core.Responses;

public class Error : IReturnable, IResponse
{
    public required string Message { get; init; }
}

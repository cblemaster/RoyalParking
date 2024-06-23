using RoyalParking.Core.Interfaces;

namespace RoyalParking.Core.Responses;

public class BadRequest : IReturnable, IResponse
{
    public string Message { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
}

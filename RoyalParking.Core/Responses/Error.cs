using RoyalParking.Core.Interfaces;

namespace RoyalParking.Core.Responses;

public class Error : IReturnable, IResponse
{
    public string Message { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
}

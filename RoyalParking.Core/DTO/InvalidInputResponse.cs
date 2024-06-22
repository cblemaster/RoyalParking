using RoyalParking.Core.Interfaces;

namespace RoyalParking.Core.DTO;

public class InvalidInputResponse : IMessageInsteadOfObject, IReturnable
{
    public required string Message { get; init; }
}

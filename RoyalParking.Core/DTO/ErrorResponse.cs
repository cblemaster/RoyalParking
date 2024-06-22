using RoyalParking.Core.Interfaces;

namespace RoyalParking.Core.DTO;

public class ErrorResponse : IMessageInsteadOfObject, IReturnable
{
    public required string Message { get; init; }
}

﻿namespace RoyalParking.Core.Validation;

public class ValidationResult
{
    public required bool IsValid { get; init; }
    public required string ErrorMessage { get; init; }
}

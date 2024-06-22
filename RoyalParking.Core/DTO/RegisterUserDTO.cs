﻿using RoyalParking.Core.Interfaces;

namespace RoyalParking.Core.DTO;

public class RegisterUserDTO : IReturnable
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string Role { get; init; }
}
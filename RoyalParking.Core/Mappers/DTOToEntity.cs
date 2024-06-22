using RoyalParking.Core.DTO;
using RoyalParking.Core.Entities;

namespace RoyalParking.Core.Mappers;

public static class DTOToEntity
{
    public static User MapRegisterUserDTOToUser(RegisterUserDTO dto)
    {
        return new()
        {
            Username = dto.Username,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
        };
    }
}

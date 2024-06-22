using RoyalParking.Core.DTO;
using RoyalParking.Core.Entities;

namespace RoyalParking.Core.Mappers;

public static class EntityToDTO
{
    public static UserDTO MapUserToUserDTO(User entity)
    {
        return new()
        {
            Id = entity.Id,
            Username = entity.Username,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Phone = entity.Phone,
            CreateDate = entity.CreateDate,
            UpdateDate = entity.UpdateDate,
            CustomerId = entity.Customer?.Id,
            ValetId = entity.Valet?.Id,
        };
    }
}

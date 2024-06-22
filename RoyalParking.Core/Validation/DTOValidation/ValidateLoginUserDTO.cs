using RoyalParking.Core.DTO;

namespace RoyalParking.Core.Validation.DTOValidation;

public static class ValidateLoginUserDTO
{
    public static IEnumerable<ValidationResult> Validate(LoginUserDTO dto)
    {
        List<ValidationResult> errors = [];
        if (!dto.Username.IsStringLengthValid(isRequired: true, maxLength: 50))
        {
            errors.Add(new()
            {
                IsValid = false,
                ErrorMessage = "Username is required and has a max length of 50 characters."
            });
        }
        if (!dto.Password.IsStringLengthValid(isRequired: true, maxLength: 50))
        {
            errors.Add(new()
            {
                IsValid = false,
                ErrorMessage = "Password is required and has a max length of 50 characters."
            });
        }
        
        errors.Add(new() { IsValid = true, ErrorMessage = string.Empty });
        return errors;
    }
}

using RoyalParking.Core.DTO;

namespace RoyalParking.Core.Validation.DTOValidation;

public static class ValidateRegisterUserDTO
{
    public static IEnumerable<ValidationResult> Validate(RegisterUserDTO dto)
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
        if (!dto.FirstName.IsStringLengthValid(isRequired: true, maxLength: 255))
        {
            errors.Add(new()
            {
                IsValid = false,
                ErrorMessage = "First name is required and has a max length of 255 characters."
            });
        }
        if (!dto.LastName.IsStringLengthValid(isRequired: true, maxLength: 255))
        {
            errors.Add(new()
            {
                IsValid = false,
                ErrorMessage = "Last Name is required and has a max length of 255 characters."
            });
        }
        if (!dto.Email.IsStringLengthValid(isRequired: true, maxLength: 255))
        {
            errors.Add(new()
            {
                IsValid = false,
                ErrorMessage = "Email is required and has a max length of 255 characters."
            });
        }
        if (!dto.Phone.IsStringLengthValid(isRequired: true, maxLength: 10))
        {
            errors.Add(new()
            {
                IsValid = false,
                ErrorMessage = "Phone is required and must be 10 characters."
            });
        }
        else if (!dto.Phone.IsStringAllNumerals())
        {
            errors.Add(new()
            {
                IsValid = false,
                ErrorMessage = "Phone must be all numerals."
            });
        }
        if (!dto.Role.IsStringLengthValid(isRequired: true, maxLength: 10))
        {
            errors.Add(new()
            {
                IsValid = false,
                ErrorMessage = "Role is required."
            });
        }
        // TODO: Unique for username, email, phone
        errors.Add(new() { IsValid = true, ErrorMessage = string.Empty });
        return errors;
    }
}

using FluentValidation;
using Proje1.DTOs;

namespace Proje1.ValidationClasses
{
    public class ProfileValidator:AbstractValidator<ProfileDto>
    {
        public ProfileValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("'{PropertyName}' is not a valid email address.").Length(3, 150).WithMessage("'{PropertyName}' must be between 3 and 150 characters long");

            RuleFor(x => x.Password).Length(8, 50).WithMessage("'{PropertyName}' must be between 8 and 50 characters long").Matches("^(?=.*[a-zA-Z])(?=.*[0-9]).+$").WithMessage("'{PropertyName}' must contain character and digit.");
        }
        
    }
}

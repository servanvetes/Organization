using FluentValidation;
using Proje1.DTOs;

namespace Proje1.ValidationClasses
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {

        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Must be of e-mail address type").Length(3, 150);

            RuleFor(x => x.Password).Length(8, 50).WithMessage("'{PropertyName}' must be between 8 and 50 characters long").Matches("^(?=.*[a-zA-Z])(?=.*[0-9]).+$").WithMessage("'{PropertyName}' must contain character and digit.");

            
        }
    }
}

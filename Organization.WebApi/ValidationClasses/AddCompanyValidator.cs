using FluentValidation;
using Organization.WebApi.DTOs;

namespace Organization.WebApi.ValidationClasses
{
    public class AddCompanyValidator: AbstractValidator<AddCompanyDto>
    {
        public AddCompanyValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Must be of e-mail address type");
        }
       
    }
}

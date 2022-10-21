using FluentValidation;
using Proje1.DTOs;

namespace Organization.App.ValidationClasses
{
    public class ActivityValidator : AbstractValidator<ActivityDto>
    {
        public ActivityValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Quota).NotEmpty().GreaterThan(0);
            RuleFor(x => x.ActivityDate).Must(NowValidDate).WithMessage(" must be greater than now");
            RuleFor(x => x.ClosedDate).Must(NowValidDate).WithMessage(" must be greater than now").GreaterThan(x => x.ActivityDate.Date);
        }

        private bool NowValidDate(DateTime value)
        {
            return value.Date > DateTime.Now.Date;
        }
    }
}

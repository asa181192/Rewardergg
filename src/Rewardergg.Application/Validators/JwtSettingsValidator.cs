using FluentValidation;
using Rewardergg.Application.Configurations;
using Rewardergg.Application.Models;

namespace Rewardergg.Application.Validators
{
    public class JwtSettingsValidator : AbstractValidator<JwtSettings>
    {
        public JwtSettingsValidator()
        {
            RuleFor(x => x.Key)
            .NotEmpty().WithMessage("Key is required.");

            RuleFor(x => x.Issuer)
                .NotEmpty().WithMessage("Issuer is required.");

            RuleFor(x => x.Audience)
                .NotEmpty().WithMessage("Audience is required.");

            RuleFor(x => x.DurationInMinutes)
                .GreaterThan(0).WithMessage("DurationInMinutes must be greater than zero.");

            RuleFor(x => x.ExpireTime)
                .GreaterThan(TimeSpan.Zero).WithMessage("ExpireTime must be a positive time span.");
        }
    }
}

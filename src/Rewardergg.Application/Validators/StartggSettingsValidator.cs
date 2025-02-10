using FluentValidation;
using Rewardergg.Application.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Application.Validators
{
    public class StartggSettingsValidator : AbstractValidator<StartggSettings>
    {
        public StartggSettingsValidator()
        {
            RuleFor(x => x.ClientId)
                .GreaterThan(0).WithMessage("ClientId must be greater than zero.");

            RuleFor(x => x.ClientSecret)
                .NotEmpty().WithMessage("ClientSecret is required.")
                .WithMessage("ClientSecret must be at least 10 characters long.");

            RuleFor(x => x.BaseUrl)
                .NotEmpty().WithMessage("BaseUrl is required.")
                .WithMessage("BaseUrl must be a valid URL.");

            RuleFor(x => x.GraphQlEndpoint)
                .NotEmpty().WithMessage("GraphQlEndpoint is required.")
                .WithMessage("GraphQlEndpoint must be a valid URL.");

            RuleFor(x => x.RedirectUrl)
                .NotEmpty().WithMessage("RedirectUrl is required.")
                .WithMessage("RedirectUrl must be a valid URL.");
        }
    }
}

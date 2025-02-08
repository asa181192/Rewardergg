using FluentValidation;
using Microsoft.Extensions.Options;

namespace Rewardergg.Api.Extensions
{
    public static class OptionsBuilderFluentValidationExtensions
    {
        public static OptionsBuilder<TOptions> ValidateFluentValidation<TOptions>(
        this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
        {
            optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(provider =>
            {
                var validator = provider.GetRequiredService<IValidator<TOptions>>();
                return new FluentValidationOptions<TOptions>(optionsBuilder.Name, validator);
            });

            return optionsBuilder;
        }

        private class FluentValidationOptions<TOptions> : IValidateOptions<TOptions> where TOptions : class
        {
            private readonly string _name;
            private readonly IValidator<TOptions> _validator;

            public FluentValidationOptions(string name, IValidator<TOptions> validator)
            {
                _name = name;
                _validator = validator;
            }

            public ValidateOptionsResult Validate(string name, TOptions options)
            {
                if (_name != null && _name != name)
                    return ValidateOptionsResult.Skip;

                var result = _validator.Validate(options);
                if (result.IsValid)
                    return ValidateOptionsResult.Success;

                var errors = result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                return ValidateOptionsResult.Fail(errors);
            }
        }
    }
}

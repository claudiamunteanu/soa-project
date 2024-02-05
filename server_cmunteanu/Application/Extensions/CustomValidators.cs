using Domain.Entities;
using FluentValidation;

namespace Application.Extensions
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, string> ValidDate<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(dateValue =>
            {
                DateTime date;
                return DateTime.TryParse(dateValue, out date);
            }).WithMessage("String is not a valid date");
        }
    }
}

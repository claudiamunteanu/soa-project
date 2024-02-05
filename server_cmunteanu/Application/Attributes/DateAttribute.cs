using System.ComponentModel.DataAnnotations;

namespace Application.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    class DateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var dateString = value as string;
            if (string.IsNullOrWhiteSpace(dateString))
            {
                return true;
            }
            DateTime result;
            var success = DateTime.TryParse(dateString, out result);
            return success;
        }
    }
}

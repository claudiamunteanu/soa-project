using FluentValidation;

namespace Application.Commands.Users.AddUser
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.UserModel).NotNull();
        }
    }
}

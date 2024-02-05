using FluentValidation;

namespace Application.Commands.Users.GetUser
{
    public class GetUserByIdValidator : AbstractValidator<GetUserByIdCommand>
    {
        public GetUserByIdValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}

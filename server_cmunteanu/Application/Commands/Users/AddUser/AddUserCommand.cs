using Application.Models.Request;
using MediatR;

namespace Application.Commands.Users.AddUser
{
    public class AddUserCommand : IRequest
    {
        public required UserCreateModel UserModel { get; set; }
    }
}

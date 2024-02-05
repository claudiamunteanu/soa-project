using Application.Models.Response;
using MediatR;

namespace Application.Commands.Users.GetUser
{
    public class GetUserByIdCommand : IRequest<UserModel>
    {
        public required string UserId { get; set; }
    }
}

using Domain.Entities;
using MediatR;

namespace Application.Commands.Users.GetAllUsers
{
    public class GetAllUsersCommand : IRequest<List<User>>
    {

    }
}

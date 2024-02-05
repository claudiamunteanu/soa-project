using Application.Repositories.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Users.GetAllUsers
{
    public class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommand, List<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<List<User>> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
        {
            var users = _userRepository.GetAll();
            return Task.FromResult(users.ToList());
        }
    }
}

using Application.Exceptions;
using Application.Models.Response;
using Application.Repositories.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Commands.Users.GetUser
{
    public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, UserModel>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserByIdCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserModel> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId);
            if (user == null)
            {
                throw new UserNotFound("User not found!");
            }

            return _mapper.Map<UserModel>(user);
        }
    }
}

using MediatR;
using Rewardergg.Application.Interfaces;
using Rewardergg.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IGenericRepository<User> _userRepository;

        public CreateUserCommandHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Points = 0
            };

            await _userRepository.AddAsync(user);

            return user.Id;
        }
    }
}

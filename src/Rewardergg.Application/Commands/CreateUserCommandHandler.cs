using MediatR;
using Rewardergg.Application.Interfaces;
using Rewardergg.Domain;

namespace Rewardergg.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Points = 0
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitAsync(cancellationToken);

            return user.Id;
        }
    }
}
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Application.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

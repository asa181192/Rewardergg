using Rewardergg.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Application.Interfaces
{
    public interface ITournamentService
    {
        Task<TournamentDto> GetTournamentByIdAsync(Guid id);
    }
}

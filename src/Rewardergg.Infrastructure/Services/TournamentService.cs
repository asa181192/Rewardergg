using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rewardergg.Application.DTOs;
using Rewardergg.Application.Interfaces;
using Rewardergg.Infrastructure.Persitence;

namespace Rewardergg.Infrastructure.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TournamentService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<TournamentDto> GetTournamentByIdAsync(Guid id)
        {
            return _mapper.Map<TournamentDto>(await _appDbContext.Tournaments.FirstOrDefaultAsync(x => x.Id == id));
        }
    }
}

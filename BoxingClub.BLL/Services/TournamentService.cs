using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Implementation.Services
{
    public class TournamentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;

        public TournamentService(IUnitOfWork uow,
                                 IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper is null");
            _database = uow ?? throw new ArgumentNullException(nameof(uow), "uow is null");
        }

        public async Task CreateStudentAsync(TournamentDTO studentDTO)
        {

        }
    }
}

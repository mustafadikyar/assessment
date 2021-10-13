using MediatR;
using Microsoft.EntityFrameworkCore;
using Rise.Assessment.Phonebook.Application.DTOs;
using Rise.Assessment.Phonebook.Application.Mappings;
using Rise.Assessment.Phonebook.Application.Queries;
using Rise.Assessment.Phonebook.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Assessment.Phonebook.Application.Handlers
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, List<PersonDTO>>
    {
        private readonly PhonebookDbContext _context;

        public GetPersonQueryHandler(PhonebookDbContext context)
        {
            _context = context;
        }

        public async Task<List<PersonDTO>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            var persons = await _context.Persons.ToListAsync();

            if (persons.Any())
            {
                var personsDto = ObjectMapper.Mapper.Map<List<PersonDTO>>(persons);
                return personsDto;
            }
            return null;
        }
    }
}

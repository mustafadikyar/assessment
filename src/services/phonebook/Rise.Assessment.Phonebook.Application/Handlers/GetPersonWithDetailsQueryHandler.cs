using MediatR;
using Microsoft.EntityFrameworkCore;
using Rise.Assessment.Phonebook.Application.DTOs;
using Rise.Assessment.Phonebook.Application.Mappings;
using Rise.Assessment.Phonebook.Application.Queries;
using Rise.Assessment.Phonebook.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Assessment.Phonebook.Application.Handlers
{
    public class GetPersonWithDetailsQueryHandler : IRequestHandler<GetPersonWithDetailsQuery, PersonDTO>
    {
        private readonly PhonebookDbContext _context;

        public GetPersonWithDetailsQueryHandler(PhonebookDbContext context)
        {
            _context = context;
        }

        public async Task<PersonDTO> Handle(GetPersonWithDetailsQuery request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.Include(person => person.PersonDetails).FirstOrDefaultAsync(person => person.Id == request.PersonId);

            if (person != null)
            {
                var personDto = ObjectMapper.Mapper.Map<PersonDTO>(person);
                return personDto;
            }
            return null;
        }
    }
}

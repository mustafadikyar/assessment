using MediatR;
using Microsoft.EntityFrameworkCore;
using Rise.Assessment.Phonebook.Application.DTOs;
using Rise.Assessment.Phonebook.Application.Mappings;
using Rise.Assessment.Phonebook.Application.Queries;
using Rise.Assessment.Phonebook.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Assessment.Phonebook.Application.Handlers
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonDTO>
    {
        private readonly PhonebookDbContext _context;

        public GetPersonQueryHandler(PhonebookDbContext context)
        {
            _context = context;
        }

        public async Task<PersonDTO> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(person => person.Id == request.PersonId);

            if (person?.Id != null)
            {
                var personDto = ObjectMapper.Mapper.Map<PersonDTO>(person);
                return personDto;
            }
            return null;
        }
    }
}

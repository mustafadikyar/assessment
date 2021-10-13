using MediatR;
using Microsoft.EntityFrameworkCore;
using Rise.Assessment.Phonebook.Application.Commands;
using Rise.Assessment.Phonebook.Application.DTOs;
using Rise.Assessment.Phonebook.Domain.PhonebookAggregate;
using Rise.Assessment.Phonebook.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Assessment.Phonebook.Application.Handlers
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, PersonDeleteDTO>
    {
        private readonly PhonebookDbContext _context;

        public DeletePersonCommandHandler(PhonebookDbContext context)
        {
            _context = context;
        }

        public async Task<PersonDeleteDTO> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            Person currentPerson = await _context.Persons.Include(person => person.PersonDetails).FirstOrDefaultAsync(person => person.Id == request.Id);

            foreach (PersonDetail detail in currentPerson.PersonDetails)
            {
                _context.PersonDetails.Remove(detail);
            }
            await _context.SaveChangesAsync();

            _context.Persons.Remove(currentPerson);
            await _context.SaveChangesAsync();

            return new PersonDeleteDTO { Id = currentPerson.Id };
        }
    }
}

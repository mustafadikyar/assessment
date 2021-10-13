using MediatR;
using Rise.Assessment.Phonebook.Application.Commands;
using Rise.Assessment.Phonebook.Application.DTOs;
using Rise.Assessment.Phonebook.Domain.PhonebookAggregate;
using Rise.Assessment.Phonebook.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Assessment.Phonebook.Application.Handlers
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, PersonCreateDTO>
    {
        private readonly PhonebookDbContext _context;

        public CreatePersonCommandHandler(PhonebookDbContext context)
        {
            _context = context;
        }

        public async Task<PersonCreateDTO> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var createdPerson = new Person(request.Name, request.Surname, request.Company);

            request.PersonDetails.ForEach(detail =>
            {
                createdPerson.AddPersonDetail(detail.PhoneNumber, detail.MailAddress, detail.Location, detail.PersonId);
            });

            await _context.AddAsync(createdPerson);
            var result = await _context.SaveChangesAsync();

            return new PersonCreateDTO { Id = createdPerson.Id };
        }
    }
}

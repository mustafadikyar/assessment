using MediatR;
using Rise.Assessment.Phonebook.Application.Commands;
using Rise.Assessment.Phonebook.Application.DTOs;
using Rise.Assessment.Phonebook.Domain.PhonebookAggregate;
using Rise.Assessment.Phonebook.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Assessment.Phonebook.Application.Handlers
{
    public class CreatePersonDetailCommandHandler : IRequestHandler<CreatePersonDetailCommand, PersonDetailCreateDTO>
    {
        private readonly PhonebookDbContext _context;

        public CreatePersonDetailCommandHandler(PhonebookDbContext context)
        {
            _context = context;
        }

        public async Task<PersonDetailCreateDTO> Handle(CreatePersonDetailCommand request, CancellationToken cancellationToken)
        {
            var createdOrderDetail = new PersonDetail(request.PhoneNumber, request.MailAddress, request.Location);

            await _context.AddAsync(createdOrderDetail);
            var result = await _context.SaveChangesAsync();

            return new PersonDetailCreateDTO { Id = createdOrderDetail.Id };
        }
    }
}
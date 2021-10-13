using MediatR;
using Rise.Assessment.Phonebook.Application.Commands;
using Rise.Assessment.Phonebook.Application.DTOs;
using Rise.Assessment.Phonebook.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Assessment.Phonebook.Application.Handlers
{
    public class DeletePersonDetailCommandHandler : IRequestHandler<DeletePersonDetailCommand, PersonDetailDeleteDTO>
    {
        private readonly PhonebookDbContext _context;

        public DeletePersonDetailCommandHandler(PhonebookDbContext context)
        {
            _context = context;
        }

        public async Task<PersonDetailDeleteDTO> Handle(DeletePersonDetailCommand request, CancellationToken cancellationToken)
        {
            var currentDetail = _context.PersonDetails.Find(request.Id);
            _context.PersonDetails.Remove(currentDetail);
            await _context.SaveChangesAsync();

            return new PersonDetailDeleteDTO { Id = currentDetail.Id };
        }
    }
}

using MediatR;
using Rise.Assessment.Phonebook.Application.DTOs;

namespace Rise.Assessment.Phonebook.Application.Commands
{
    public class DeletePersonDetailCommand : IRequest<PersonDetailDeleteDTO>
    {
        public int Id { get; set; }
    }
}

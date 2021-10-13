using MediatR;
using Rise.Assessment.Phonebook.Application.DTOs;

namespace Rise.Assessment.Phonebook.Application.Commands
{
    public class DeletePersonCommand : IRequest<PersonDeleteDTO>
    {
        public int Id { get; set; }
    }
}

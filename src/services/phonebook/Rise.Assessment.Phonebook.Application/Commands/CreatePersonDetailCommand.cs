using MediatR;
using Rise.Assessment.Phonebook.Application.DTOs;

namespace Rise.Assessment.Phonebook.Application.Commands
{
    public class CreatePersonDetailCommand : IRequest<PersonDetailCreateDTO>
    {
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public string Location { get; set; }
        public int PersonId { get; set; }
    }
}

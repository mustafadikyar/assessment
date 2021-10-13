using MediatR;
using Rise.Assessment.Phonebook.Application.DTOs;

namespace Rise.Assessment.Phonebook.Application.Commands
{
    public class CreatePersonDetailCommand : IRequest<PersonDetailCreateDTO>
    {
        public string PhoneNumber { get; private set; }
        public string MailAddress { get; private set; }
        public string Location { get; private set; }
    }
}

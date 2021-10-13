using MediatR;
using Rise.Assessment.Phonebook.Application.DTOs;
using System.Collections.Generic;

namespace Rise.Assessment.Phonebook.Application.Commands
{
    public class CreatePersonCommand : IRequest<PersonCreateDTO>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public List<PersonDetailDTO> PersonDetails { get; set; }
    }
}

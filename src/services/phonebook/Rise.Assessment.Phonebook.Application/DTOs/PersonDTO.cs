using System.Collections.Generic;

namespace Rise.Assessment.Phonebook.Application.DTOs
{
    public class PersonDTO
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Company { get; private set; }
        public List<PersonDetailDTO> PersonDetails { get; set; }
    }
}

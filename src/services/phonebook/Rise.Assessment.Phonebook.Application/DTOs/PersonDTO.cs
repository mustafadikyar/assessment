using System.Collections.Generic;

namespace Rise.Assessment.Phonebook.Application.DTOs
{
    public class PersonDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public List<PersonDetailDTO> PersonDetails { get; set; }
    }
}

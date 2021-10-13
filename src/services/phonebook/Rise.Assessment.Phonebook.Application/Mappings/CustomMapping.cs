using AutoMapper;
using Rise.Assessment.Phonebook.Application.DTOs;
using Rise.Assessment.Phonebook.Domain.PhonebookAggregate;

namespace Rise.Assessment.Phonebook.Application.Mappings
{
    internal class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<PersonDetail, PersonDetailDTO>().ReverseMap();
        }
    }
}

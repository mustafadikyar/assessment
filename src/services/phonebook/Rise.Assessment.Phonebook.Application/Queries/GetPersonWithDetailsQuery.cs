using MediatR;
using Rise.Assessment.Phonebook.Application.DTOs;

namespace Rise.Assessment.Phonebook.Application.Queries
{
    public class GetPersonWithDetailsQuery : IRequest<PersonDTO>
    {
        public int PersonId { get; set; }
    }
}

using MediatR;
using Rise.Assessment.Phonebook.Application.DTOs;
using System.Collections.Generic;

namespace Rise.Assessment.Phonebook.Application.Queries
{
    public class GetPersonQuery : IRequest<PersonDTO>
    {
        public int PersonId { get; set; }
    }
}

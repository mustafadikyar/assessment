using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rise.Assessment.Phonebook.Application.Commands;
using Rise.Assessment.Phonebook.Application.Queries;
using System.Threading.Tasks;

namespace Rise.Assessment.Phonebook.API.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> GetPerson(int personId)
        {
            var response = await _mediator.Send(new GetPersonQuery { PersonId = personId });
            return Ok(response);
        }

        [HttpGet("{personId}/details")]
        public async Task<IActionResult> GetPersonWithDetails(int personId)
        {
            var response = await _mediator.Send(new GetPersonWithDetailsQuery { PersonId = personId });
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson(CreatePersonCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("detail")]
        public async Task<IActionResult> CreatePersonDetail(CreatePersonDetailCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson(DeletePersonCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("detail")]
        public async Task<IActionResult> DeletePersonDetail(DeletePersonDetailCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
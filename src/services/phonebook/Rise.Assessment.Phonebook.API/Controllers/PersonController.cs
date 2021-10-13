using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rise.Assessment.Phonebook.Application.Commands;
using Rise.Assessment.Phonebook.Application.DTOs;
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
        [ProducesResponseType(typeof(PersonDTO), 200)]
        [ProducesResponseType(typeof(object), 500)]
        public async Task<IActionResult> GetPerson(int personId)
        {
            var response = await _mediator.Send(new GetPersonQuery { PersonId = personId });

            if (response != null)
                return Ok(response);

            return NotFound();
        }

        [HttpGet("{personId}/details")]
        [ProducesResponseType(typeof(PersonDTO), 200)]
        [ProducesResponseType(typeof(object), 500)]
        public async Task<IActionResult> GetPersonWithDetails(int personId)
        {
            var response = await _mediator.Send(new GetPersonWithDetailsQuery { PersonId = personId });

            if (response != null)
                return Ok(response);

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(PersonCreateDTO), 200)]
        [ProducesResponseType(typeof(object), 500)]
        public async Task<IActionResult> CreatePerson(CreatePersonCommand command)
        {
            var response = await _mediator.Send(command);
            return Created("", response);
        }

        [HttpPost("detail")]
        [ProducesResponseType(typeof(PersonDetailCreateDTO), 200)]
        [ProducesResponseType(typeof(object), 500)]
        public async Task<IActionResult> CreatePersonDetail(CreatePersonDetailCommand command)
        {
            var response = await _mediator.Send(command);
            return Created("", response);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(object), 500)]
        public async Task<IActionResult> DeletePerson(DeletePersonCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("detail")]
        [ProducesResponseType(typeof(object), 500)]
        public async Task<IActionResult> DeletePersonDetail(DeletePersonDetailCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorialProject.Interfaces;
using TutorialProject.Models;
using TutorialProject.Services;

namespace TutorialProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        /// <summary>
        /// Get a list of all people
        /// </summary>
        /// <returns>List of all people</returns>
        [HttpGet("/GetPeople")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            ActionResult returnValue = BadRequest("No people found.");
            IEnumerable<Person> people = await _peopleService.GetPeople();

            if (people != null)
            {
                returnValue = Ok(people);
            }

            return returnValue;
        }

        [HttpGet("/GetPerson")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            ActionResult returnValue = NotFound("Person not found."); ;
            Person person = await _peopleService.GetPerson(id);

            if (person != null)
            {
                returnValue = Ok(person);
            } 

            return returnValue;
        }

        /// <summary>
        /// Edit a person
        /// </summary>
        /// <param name="id">Person ID</param>
        /// <param name="person">Person</param>
        /// <returns>IActionResult</returns>
        [HttpPut("/EditPerson")]
        public async Task<IActionResult> EditPerson(Guid id, Person person)
        {
            IActionResult returnValue = BadRequest();
            EditPersonResponse response = await _peopleService.EditPerson(id, person);

            switch (response.ResponseType)
            {
                case EditPersonResponseType.BadRequest:
                    returnValue = BadRequest(response.Message);
                    break;
                case EditPersonResponseType.NoContent:
                    returnValue = Ok(person);
                    break;
                case EditPersonResponseType.NotFound:
                    returnValue = NotFound(response.Message);
                    break;
            }

            return returnValue;
        }

        /// <summary>
        /// Add a person
        /// </summary>
        /// <param name="person">Person</param>
        /// <returns>IActionResult</returns>
        [HttpPost("/AddPerson")]
        public async Task<ActionResult<Person>> AddPerson(Person person)
        {
            var createdPerson = await _peopleService.AddPerson(person);

            if (createdPerson != null)
            {
                return Created(person.Id.ToString(), person);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="id">Person ID</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            if (await _peopleService.DeletePerson(id))
            {
                return Ok("Person deleted");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

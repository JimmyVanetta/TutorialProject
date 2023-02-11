using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using TutorialProject.Interfaces;
using TutorialProject.Models;

namespace TutorialProject.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly PersonContext _context;

        public PeopleService(PersonContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public async Task<Person> AddPerson(Person person)
        {
            try
            {
                _context.People.Add(person);
                await _context.SaveChangesAsync();

                return person;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeletePerson(Guid id)
        {
            try
            {
                var person = await _context.People.FindAsync(id);
                if (person == null)
                {
                    return false;
                }

                _context.People.Remove(person);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<EditPersonResponse> EditPerson(Guid id, Person person)
        {
            EditPersonResponse returnValue = new();
            returnValue.Person = null;
            try
            {
                if (id == person.Id)
                {
                    _context.Entry(person).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                    returnValue.Person = person;
                    returnValue.Message = "Person successfully updated";
                    returnValue.ResponseType = EditPersonResponseType.NoContent;
                }
                else
                {
                    returnValue.Message = "Person ID mismatch";
                    returnValue.ResponseType = EditPersonResponseType.BadRequest;
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                returnValue.ResponseType = EditPersonResponseType.NotFound;
                if (!PersonExists(id))
                {
                    returnValue.Message = $"Person not found. {ex.Message}";
                }
                else
                {
                    returnValue.Message = $"{ex.Message}";
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Person>> GetPeople()
        {
            try
            {
                return await _context.People.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Person> GetPerson(Guid id)
        {
            Person returnValue = null;
            try
            {
                var person = await _context.People.FindAsync(id);

                if (person != null)
                {
                    returnValue = person;
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                return returnValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool PersonExists(Guid id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using TutorialProject.Models;

namespace TutorialProject.Interfaces
{
    public interface IPeopleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public Task<Person> AddPerson(Person person);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeletePerson(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        Task<EditPersonResponse> EditPerson(Guid id, Person person);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Person>> GetPeople();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Person Id</param>
        /// <returns>Person</returns>
        public Task<Person> GetPerson(Guid id);
    }
}

using Data_WebApplication2.Models;
using System.Collections.Generic;

namespace Data_WebApplication2
{
    public interface IMyDbRepository
    {
        List<Person> GetPersons();
        Person GetPersonById(int id);
        void AddPerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
        bool Save();
    }
}
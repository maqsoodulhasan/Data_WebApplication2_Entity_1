using Data_WebApplication2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_WebApplication2
{
    public class MyDbRepository : IMyDbRepository
    {
        private readonly MyDbContext _context;

        public MyDbRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<Person> GetPersons()
        {
            return _context.Persons.ToList();
        }

        public Person GetPersonById(int id)
        {
            return _context.Persons.Where(x => x.Id == id).FirstOrDefault();
        }
        
        public void AddPerson(Person person)
        {
            _context.Persons.Add(person);
        }
        
        public void UpdatePerson(Person person)
        {
            _context.Persons.Update(person);
        }
        
        public void DeletePerson(Person person)
        {
            _context.Persons.Remove(person);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
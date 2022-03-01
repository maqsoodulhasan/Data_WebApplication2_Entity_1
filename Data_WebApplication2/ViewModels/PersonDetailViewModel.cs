
using System.Collections.Generic;

namespace Data_WebApplication2.ViewModels
{
    public class PersonDetailViewModel
    {
        public CreatePersonViewModel CreatePersonViewModel { get; set; }
        public List<PeopleViewModel> PeopleViewModel { get; set; }
    public PersonDetailViewModel ()
        {
            PeopleViewModel = new List<PeopleViewModel> ();
            CreatePersonViewModel = new CreatePersonViewModel();
        }
    }
    
}

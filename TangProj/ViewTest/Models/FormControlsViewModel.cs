using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewTest.Models
{
    public class FormControlsViewModel
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public List<Course> Courses { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public List<Country> CountryList { get; set; }
        public string Description { get; set; }
    }
    public class Country
    {
        public string ID { get; set; }
        public string Name { get; set; }
 
    }
   
    public class Course
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
 
    }
}
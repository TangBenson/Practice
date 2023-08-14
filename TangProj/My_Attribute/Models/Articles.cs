using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Attribute.Models
{
    public class Articles
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostOn { get; set; }
    }
}
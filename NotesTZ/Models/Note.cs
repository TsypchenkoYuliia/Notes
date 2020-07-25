using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesTZ.Models
{
    public class Note
    {
        public Guid Id {get; set;}
        public string Heading { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

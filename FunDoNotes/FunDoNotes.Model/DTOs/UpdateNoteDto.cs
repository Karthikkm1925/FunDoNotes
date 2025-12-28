using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.Model.DTOs
{
    public class UpdateNoteDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPinned { get; set; }
        public string Color { get; set; }
    }
}

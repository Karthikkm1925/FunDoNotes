using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.Model.DTOs
{
    public class CreateNoteDto
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPinned { get; set; } = false;

        public string Color { get; set; }
    }
}

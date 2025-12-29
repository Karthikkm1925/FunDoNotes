using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.Model.DTOs
{
    public class NoteResponseDto
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPinned { get; set; }
        public bool IsArchived { get; set; }
        public bool IsTrashed { get; set; }
        public string Color { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

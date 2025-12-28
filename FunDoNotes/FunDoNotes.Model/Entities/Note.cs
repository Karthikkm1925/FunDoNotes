using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.Model.Entities
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

         
        public bool IsPinned { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        public bool IsTrashed { get; set; } = false;

        public string Color { get; set; }

         
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

         
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

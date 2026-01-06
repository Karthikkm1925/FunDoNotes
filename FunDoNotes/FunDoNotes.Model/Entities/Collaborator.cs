using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.Model.Entities
{
    public class Collaborator
    {
        [Key]
        public int CollaboratorId { get; set; }
        public string Email { get; set; } = null!;
        // Note being shared
        public int NoteId { get; set; }
        public Note Note { get; set; } = null!;

        // Owner of the note
        public int OwnerUserId { get; set; }
        public User OwnerUser { get; set; } = null!;

        // User with whom note is shared
        public int SharedUserId { get; set; }
        public User SharedUser { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

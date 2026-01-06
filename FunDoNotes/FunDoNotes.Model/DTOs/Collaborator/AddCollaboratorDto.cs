using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.Model.DTOs.Collaborator
{
    public class AddCollaboratorDto
    {
        public int NoteId { get; set; }
        public string Email { get; set; }
    }
}

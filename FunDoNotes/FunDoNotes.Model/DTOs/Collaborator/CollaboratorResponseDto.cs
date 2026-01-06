using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.Model.DTOs.Collaborator
{
    public class CollaboratorResponseDto
    {
        public int CollaboratorId { get; set; }
        public string Email { get; set; } = null!;
        public int SharedUserId { get; set; }
    }
}

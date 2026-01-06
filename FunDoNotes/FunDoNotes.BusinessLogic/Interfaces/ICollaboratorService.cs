using FunDoNotes.Model.DTOs;
using FunDoNotes.Model.DTOs.Collaborator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.BusinessLogic.Interfaces
{
    public interface ICollaboratorService
    {
        void AddCollaborator(AddCollaboratorDto dto, int ownerUserId);
        List<CollaboratorResponseDto> GetCollaborators(int noteId, int ownerUserId);
        void RemoveCollaborator(int collaboratorId, int ownerUserId);
        void RemoveCollaboratorByEmail(int noteId, string email, int ownerUserId);
        List<NoteResponseDto> GetSharedNotes(int userId);
    }
}

using FunDoNotes.Model.DTOs;
using FunDoNotes.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.BusinessLogic.Interfaces
{
    public interface INoteService
    {
        void CreateNote(CreateNoteDto dto, int userId);
        IEnumerable<Note> GetAllNotes(int userId);
        Note GetNoteById(int noteId, int userId);
        void UpdateNote(int noteId, UpdateNoteDto dto, int userId);
        void MoveToTrash(int noteId, int userId);

        IEnumerable<Note> GetTrashedNotes(int userId);
        void RestoreNote(int noteId, int userId);
        void PermanentDelete(int noteId, int userId);

        void ArchiveNote(int noteId, int userId);
        void UnarchiveNote(int noteId, int userId);

        void PinNote(int noteId, int userId);
        void UnpinNote(int noteId, int userId);

        void UpdateColor(int noteId, string color, int userId);
        IEnumerable<Note> GetArchivedNotes(int userId);


    }
}

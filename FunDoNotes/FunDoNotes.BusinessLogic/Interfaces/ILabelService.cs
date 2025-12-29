using FunDoNotes.Model.DTOs.Label;
using FunDoNotes.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.BusinessLogic.Interfaces
{
    public interface ILabelService
    {
        void CreateLabel(CreateLabelDto dto, int userId);
        List<LabelResponseDto> GetLabels(int userId);
        void UpdateLabel(int labelId, UpdateLabelDto dto, int userId);
        void DeleteLabel(int labelId, int userId);
        void AddLabelToNote(int labelId, int noteId, int userId);
        List<NoteResponseDto> GetNotesByLabel(int labelId, int userId);
        void RemoveLabelFromNote(int labelId, int noteId, int userId);


    }
}

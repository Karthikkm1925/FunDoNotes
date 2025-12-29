using FunDoNotes.BusinessLogic.Interfaces;
using FunDoNotes.DataAccess.Context;
using FunDoNotes.Model.DTOs.Label;
using FunDoNotes.Model.DTOs;
using FunDoNotes.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunDoNotes.BusinessLogic.Services
{
    public class LabelService : ILabelService
    {
        private readonly FunDoNotesDbContext _context;

        public LabelService(FunDoNotesDbContext context)
        {
            _context = context;
        }

         
        public void CreateLabel(CreateLabelDto dto, int userId)
        {
            var label = new Label
            {
                LabelName = dto.LabelName,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Labels.Add(label);
            _context.SaveChanges();
        }

         
        public List<LabelResponseDto> GetLabels(int userId)
        {
            return _context.Labels
                .Where(l => l.UserId == userId)
                .Select(l => new LabelResponseDto
                {
                    LabelId = l.LabelId,
                    LabelName = l.LabelName
                })
                .ToList();
        }

        public void UpdateLabel(int labelId, UpdateLabelDto dto, int userId)
        {
            var label = _context.Labels
                .FirstOrDefault(l => l.LabelId == labelId && l.UserId == userId);

            if (label == null)
                throw new Exception("Label not found or access denied");

            label.LabelName = dto.LabelName;
            label.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }

        public void DeleteLabel(int labelId, int userId)
        {
            var label = _context.Labels
                .FirstOrDefault(l => l.LabelId == labelId && l.UserId == userId);

            if (label == null)
                throw new Exception("Label not found or access denied");

            // Remove label-note mappings first
            var mappings = _context.NoteLabels
                .Where(nl => nl.LabelId == labelId);

            _context.NoteLabels.RemoveRange(mappings);

            _context.Labels.Remove(label);
            _context.SaveChanges();
        }

        public void AddLabelToNote(int labelId, int noteId, int userId)
        {
            var note = _context.Notes
                .FirstOrDefault(n => n.NoteId == noteId && n.UserId == userId);

            if (note == null)
                throw new Exception("Note not found");

            var label = _context.Labels
                .FirstOrDefault(l => l.LabelId == labelId && l.UserId == userId);

            if (label == null)
                throw new Exception("Label not found");

            bool exists = _context.NoteLabels
                .Any(nl => nl.NoteId == noteId && nl.LabelId == labelId);

            if (exists)
                return;

            _context.NoteLabels.Add(new NoteLabel
            {
                NoteId = noteId,
                LabelId = labelId
            });

            _context.SaveChanges();
        }

        public List<NoteResponseDto> GetNotesByLabel(int labelId, int userId)
        {
            return _context.NoteLabels
                .Where(nl => nl.LabelId == labelId && nl.Note.UserId == userId)
                .Select(nl => new NoteResponseDto
                {
                    NoteId = nl.Note.NoteId,
                    Title = nl.Note.Title,
                    Description = nl.Note.Description,
                    Color = nl.Note.Color
                })
                .ToList();
        }

        public void RemoveLabelFromNote(int labelId, int noteId, int userId)
        {
            var mapping = _context.NoteLabels
                .FirstOrDefault(nl =>
                    nl.LabelId == labelId &&
                    nl.NoteId == noteId &&
                    nl.Note.UserId == userId);

            if (mapping == null)
                throw new Exception("Mapping not found");

            _context.NoteLabels.Remove(mapping);
            _context.SaveChanges();
        }

    }
}

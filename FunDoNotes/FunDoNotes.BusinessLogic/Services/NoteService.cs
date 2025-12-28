using FunDoNotes.BusinessLogic.Interfaces;
using FunDoNotes.DataAccess.Context;
using FunDoNotes.Model.DTOs;
using FunDoNotes.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.BusinessLogic.Services
{
    public class NoteService : INoteService
    {
        private readonly FunDoNotesDbContext _context;

        public NoteService(FunDoNotesDbContext context)
        {
            _context = context;
        }

        public void CreateNote(CreateNoteDto dto, int userId)
        {
            var note = new Note
            {
                Title = dto.Title,
                Description = dto.Description,
                IsPinned = dto.IsPinned,
                Color = dto.Color,
                UserId = userId
            };

            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public IEnumerable<Note> GetAllNotes(int userId)
        {
            return _context.Notes
                .Where(n => n.UserId == userId && !n.IsTrashed)
                .OrderByDescending(n => n.IsPinned)
                .ThenByDescending(n => n.CreatedAt)
                .ToList();
        }

        public Note GetNoteById(int noteId, int userId)
        {
            var note = _context.Notes
                .FirstOrDefault(n =>
                    n.NoteId == noteId &&
                    n.UserId == userId &&
                    !n.IsTrashed);

            if (note == null)
                throw new Exception("Note not found");

            return note;
        }

        public void UpdateNote(int noteId, UpdateNoteDto dto, int userId)
        {
            var note = _context.Notes
                .FirstOrDefault(n =>
                    n.NoteId == noteId &&
                    n.UserId == userId &&
                    !n.IsTrashed);

            if (note == null)
                throw new Exception("Note not found");

            note.Title = dto.Title ?? note.Title;
            note.Description = dto.Description ?? note.Description;
            note.IsPinned = dto.IsPinned;
            note.Color = dto.Color ?? note.Color;
            note.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }

        public void MoveToTrash(int noteId, int userId)
        {
            var note = _context.Notes
                .FirstOrDefault(n =>
                    n.NoteId == noteId &&
                    n.UserId == userId &&
                    !n.IsTrashed);

            if (note == null)
                throw new Exception("Note not found");

            note.IsTrashed = true;
            note.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }

        public IEnumerable<Note> GetTrashedNotes(int userId)
        {
            return _context.Notes
                .Where(n => n.UserId == userId && n.IsTrashed)
                .OrderByDescending(n => n.UpdatedAt)
                .ToList();
        }

        public void RestoreNote(int noteId, int userId)
        {
            var note = _context.Notes
                .FirstOrDefault(n =>
                    n.NoteId == noteId &&
                    n.UserId == userId &&
                    n.IsTrashed);

            if (note == null)
                throw new Exception("Note not found in trash");

            note.IsTrashed = false;
            note.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }

        public void PermanentDelete(int noteId, int userId)
        {
            var note = _context.Notes
                .FirstOrDefault(n =>
                    n.NoteId == noteId &&
                    n.UserId == userId &&
                    n.IsTrashed);

            if (note == null)
                throw new Exception("Note not found in trash");

            _context.Notes.Remove(note);
            _context.SaveChanges();
        }

        private Note GetUserNote(int noteId, int userId)
        {
            var note = _context.Notes
                .FirstOrDefault(n =>
                    n.NoteId == noteId &&
                    n.UserId == userId &&
                    !n.IsTrashed);

            if (note == null)
                throw new Exception("Note not found");

            return note;
        }

        public void ArchiveNote(int noteId, int userId)
        {
            var note = GetUserNote(noteId, userId);
            note.IsArchived = true;
            note.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public void UnarchiveNote(int noteId, int userId)
        {
            var note = GetUserNote(noteId, userId);
            note.IsArchived = false;
            note.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public void PinNote(int noteId, int userId)
        {
            var note = GetUserNote(noteId, userId);
            note.IsPinned = true;
            note.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public void UnpinNote(int noteId, int userId)
        {
            var note = GetUserNote(noteId, userId);
            note.IsPinned = false;
            note.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public void UpdateColor(int noteId, string color, int userId)
        {
            var note = GetUserNote(noteId, userId);
            note.Color = color;
            note.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public IEnumerable<Note> GetArchivedNotes(int userId)
        {
            return _context.Notes
                .Where(n =>
                    n.UserId == userId &&
                    n.IsArchived &&
                    !n.IsTrashed)
                .OrderByDescending(n => n.UpdatedAt)
                .ToList();
        }

    }
}

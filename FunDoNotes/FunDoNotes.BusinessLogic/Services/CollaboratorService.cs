using FunDoNotes.BusinessLogic.Interfaces;
using FunDoNotes.DataAccess.Context;
using FunDoNotes.Model.DTOs;
using FunDoNotes.Model.DTOs.Collaborator;
using FunDoNotes.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.BusinessLogic.Services
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly FunDoNotesDbContext _context;

        public CollaboratorService(FunDoNotesDbContext context)
        {
            _context = context;
        }

        public void AddCollaborator(AddCollaboratorDto dto, int ownerUserId)
        {
            var note = _context.Notes
                .FirstOrDefault(n => n.NoteId == dto.NoteId && n.UserId == ownerUserId);

            if (note == null)
                throw new Exception("Note not found or access denied");

            var sharedUser = _context.Users
                .FirstOrDefault(u => u.Email == dto.Email);

            if (sharedUser == null)
                throw new Exception("User not found");

            var exists = _context.Collaborators.Any(c =>
                c.NoteId == dto.NoteId &&
                c.SharedUserId == sharedUser.UserId);

            if (exists)
                throw new Exception("Collaborator already added");

            var collaborator = new Collaborator
            {
                NoteId = dto.NoteId,
                OwnerUserId = ownerUserId,
                SharedUserId = sharedUser.UserId
            };

            _context.Collaborators.Add(collaborator);
            _context.SaveChanges();
        }

        public List<CollaboratorResponseDto> GetCollaborators(int noteId, int ownerUserId)
        {
            var noteExists = _context.Notes
       .Any(n => n.NoteId == noteId && n.UserId == ownerUserId);

            if (!noteExists)
                throw new Exception("Note not found or access denied");

            return _context.Collaborators
                .Where(c => c.NoteId == noteId)
                .Select(c => new CollaboratorResponseDto
                {
                    CollaboratorId = c.CollaboratorId,
                    Email = c.Email,
                    SharedUserId = c.SharedUserId
                })
                .ToList();
        }

        public void RemoveCollaborator(int collaboratorId, int ownerUserId)
        {
            var collaborator = _context.Collaborators
        .FirstOrDefault(c =>
            c.CollaboratorId == collaboratorId &&
            c.OwnerUserId == ownerUserId);

            if (collaborator == null)
                throw new Exception("Collaborator not found or access denied");

            _context.Collaborators.Remove(collaborator);
            _context.SaveChanges();
        }

        public void RemoveCollaboratorByEmail(int noteId, string email, int ownerUserId)
        {
            var collaborator = _context.Collaborators
        .FirstOrDefault(c =>
            c.NoteId == noteId &&
            c.Email == email &&
            c.OwnerUserId == ownerUserId);

            if (collaborator == null)
                throw new Exception("Collaborator not found or access denied");

            _context.Collaborators.Remove(collaborator);
            _context.SaveChanges();
        }

        public List<NoteResponseDto> GetSharedNotes(int userId)
        {
            return _context.Collaborators
   .Where(c => c.SharedUserId == userId)
   .Select(c => c.Note)
   .Select(n => new NoteResponseDto
   {
       NoteId = n.NoteId,
       Title = n.Title,
       Description = n.Description,
       Color = n.Color,
       IsArchived = n.IsArchived,
       IsPinned = n.IsPinned,
       CreatedAt = n.CreatedAt
   })
   .ToList();
        }

    }
}

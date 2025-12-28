using FunDoNotes.BusinessLogic.Interfaces;
using FunDoNotes.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FunDoNotes.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/notes")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost]
        public IActionResult CreateNote(CreateNoteDto dto)
        {
            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            _noteService.CreateNote(dto, userId);

            return Ok("Note created successfully");
        }

        [HttpGet]
        public IActionResult GetAllNotes()
        {
            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var notes = _noteService.GetAllNotes(userId);
            return Ok(notes);
        }

        [HttpGet("{noteId}")]
        public IActionResult GetNoteById(int noteId)
        {
            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var note = _noteService.GetNoteById(noteId, userId);
            return Ok(note);
        }

        [HttpPut("{noteId}")]
        public IActionResult UpdateNote(int noteId, UpdateNoteDto dto)
        {
            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            _noteService.UpdateNote(noteId, dto, userId);
            return Ok("Note updated successfully");
        }

        [HttpDelete("{noteId}")]
        public IActionResult MoveToTrash(int noteId)
        {
            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            _noteService.MoveToTrash(noteId, userId);
            return Ok("Note moved to trash");
        }

        [HttpGet("trash")]
        public IActionResult GetTrashedNotes()
        {
            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var notes = _noteService.GetTrashedNotes(userId);
            return Ok(notes);
        }

        [HttpPatch("{noteId}/restore")]
        public IActionResult RestoreNote(int noteId)
        {
            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            _noteService.RestoreNote(noteId, userId);
            return Ok("Note restored successfully");
        }

        [HttpDelete("{noteId}/permanent")]
        public IActionResult PermanentDelete(int noteId)
        {
            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            _noteService.PermanentDelete(noteId, userId);
            return Ok("Note permanently deleted");
        }

        [HttpPatch("{noteId}/archive")]
        public IActionResult Archive(int noteId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            _noteService.ArchiveNote(noteId, userId);
            return Ok("Note archived");
        }

        [HttpPatch("{noteId}/unarchive")]
        public IActionResult Unarchive(int noteId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            _noteService.UnarchiveNote(noteId, userId);
            return Ok("Note unarchived");
        }

        [HttpPatch("{noteId}/pin")]
        public IActionResult Pin(int noteId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            _noteService.PinNote(noteId, userId);
            return Ok("Note pinned");
        }

        [HttpPatch("{noteId}/unpin")]
        public IActionResult Unpin(int noteId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            _noteService.UnpinNote(noteId, userId);
            return Ok("Note unpinned");
        }

        [HttpPatch("{noteId}/color")]
        public IActionResult UpdateColor(int noteId, [FromBody] string color)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            _noteService.UpdateColor(noteId, color, userId);
            return Ok("Color updated");
        }

        [HttpGet("archive")]
        public IActionResult GetArchivedNotes()
        {
            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var notes = _noteService.GetArchivedNotes(userId);
            return Ok(notes);
        }

    }
}

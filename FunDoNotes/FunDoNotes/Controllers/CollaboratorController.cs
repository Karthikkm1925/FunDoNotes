using FunDoNotes.BusinessLogic.Interfaces;
using FunDoNotes.Model.DTOs.Collaborator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FunDoNotes.Controllers
{
    [ApiController]
    [Route("api/collaborators")]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorService _service;

        public CollaboratorController(ICollaboratorService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult AddCollaborator(AddCollaboratorDto dto)
        {
            int userId = GetUserIdFromToken();
            _service.AddCollaborator(dto, userId);
            return Ok("Collaborator added");
        }
        private int GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new Exception("UserId not found in token");

            return int.Parse(userIdClaim.Value);
        }


        [HttpGet("{noteId}")]
        public IActionResult GetCollaborators(int noteId)
        {
            int userId = GetUserIdFromToken();
            return Ok(_service.GetCollaborators(noteId, userId));
        }

        [HttpDelete("{collaboratorId}")]
        public IActionResult RemoveCollaborator(int collaboratorId)
        {
            int userId = GetUserIdFromToken();
            _service.RemoveCollaborator(collaboratorId, userId);
            return Ok("Collaborator removed");
        }

        [HttpDelete("{noteId}/{email}")]
        public IActionResult RemoveCollaboratorByEmail(int noteId, string email)
        {
            int userId = GetUserIdFromToken();
            _service.RemoveCollaboratorByEmail(noteId, email, userId);
            return Ok("Collaborator removed");
        }

        [HttpGet("shared-notes")]
        public IActionResult GetSharedNotes()
        {
            int userId = GetUserIdFromToken();
            return Ok(_service.GetSharedNotes(userId));
        }
    }

}

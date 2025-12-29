using FunDoNotes.BusinessLogic.Interfaces;
using FunDoNotes.Model.DTOs.Label;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FunDoNotes.Controllers
{
    [ApiController]
    [Route("api/labels")]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelService _labelService;

        public LabelController(ILabelService labelService)
        {
            _labelService = labelService;
        }

        [HttpPost]
        public IActionResult CreateLabel(CreateLabelDto dto)
        {
            int userId = GetUserIdFromToken();

            _labelService.CreateLabel(dto, userId);

            return Ok("Label created successfully");
        }

        private int GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new Exception("UserId not found in token");

            return int.Parse(userIdClaim.Value);
        }
        [HttpGet]
        public IActionResult GetLabels()
        {
            int userId = GetUserIdFromToken();

            var labels = _labelService.GetLabels(userId);

            return Ok(labels);
        }
        [HttpPut("{labelId}")]
        public IActionResult UpdateLabel(int labelId, UpdateLabelDto dto)
        {
            int userId = GetUserIdFromToken();
            _labelService.UpdateLabel(labelId, dto, userId);
            return Ok("Label updated successfully");
        }

        [HttpDelete("{labelId}")]
        public IActionResult DeleteLabel(int labelId)
        {
            int userId = GetUserIdFromToken();

            _labelService.DeleteLabel(labelId, userId);

            return Ok("Label deleted successfully");
        }

        [HttpPost("{labelId}/notes/{noteId}")]
        public IActionResult AddLabelToNote(int labelId, int noteId)
        {
            int userId = GetUserIdFromToken();
            _labelService.AddLabelToNote(labelId, noteId, userId);
            return Ok("Label added to note");
        }

        [HttpGet("{labelId}/notes")]
        public IActionResult GetNotesByLabel(int labelId)
        {
            int userId = GetUserIdFromToken();
            return Ok(_labelService.GetNotesByLabel(labelId, userId));
        }

        [HttpDelete("{labelId}/notes/{noteId}")]
        public IActionResult RemoveLabelFromNote(int labelId, int noteId)
        {
            int userId = GetUserIdFromToken();
            _labelService.RemoveLabelFromNote(labelId, noteId, userId);
            return Ok("Label removed from note");
        }

    }
}

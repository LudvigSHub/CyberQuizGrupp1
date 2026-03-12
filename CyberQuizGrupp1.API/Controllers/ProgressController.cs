using CyberQuizGrupp1.BLL.Interfaces;
using CyberQuizGrupp1.SHARED.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CyberQuizGrupp1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressService _progressService;

        public ProgressController(IProgressService progressService)
        {
            _progressService = progressService;
        }

        // GET: api/progress/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserProgressDTO>> GetProgressAsync(string userId)
        {
            // enkel validering
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest("userId cannot be empty");

            var progress = await _progressService.GetUserProgressAsync(userId);

            return Ok(progress);
        }
    }
}
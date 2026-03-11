using CyberQuizGrupp1.BLL.Interfaces;
using CyberQuizGrupp1.SHARED.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CyberQuizGrupp1.API.Controllers
{
    //api controller för coaching-funktionalitet
    //hanterar endpoints för att hämta failade subkategorier och generera coaching
    [ApiController]
    [Route("api/[controller]")]
    public class CoachingController : ControllerBase
    {
        private readonly ICoachingService _coachingService;

        //konstruktor som tar emot coachingservice via dependency injection
        public CoachingController(ICoachingService coachingService)
        {
            _coachingService = coachingService;
        }

        //hämtar alla subkategorier som användaren har försökt men inte klarat
        //GET: api/coaching/failed?userId=abc123
        [HttpGet("failed")]
        public async Task<ActionResult<List<CoachingItemDTO>>> GetFailedSubCategories([FromQuery] string userId)
        {
            //validera att userid inte är tomt
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest("userId cannot be empty");

            //hämta failade subkategorier från service
            var result = await _coachingService.GetFailedSubCategoriesAsync(userId);
            return Ok(result);
        }

        //genererar coaching för en specifik subkategori och användare
        //GET: api/coaching/2?userId=abc123
        [HttpGet("{subCategoryId}")]
        public async Task<ActionResult<CoachingResponseDTO>> GetCoaching(int subCategoryId, [FromQuery] string userId)
        {
            //validera input-parametrar
            if (subCategoryId <= 0)
                return BadRequest("subCategoryId must be greater than 0");

            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest("userId cannot be empty");

            //hämta coaching från service
            var result = await _coachingService.GetCoachingAsync(subCategoryId, userId);

            //returnera 404 om ingen coaching-data hittas
            if (result == null)
                return NotFound("No coaching data found");

            return Ok(result);
        }
    }
}
using CyberQuizGrupp1.Services.Interfaces;
using CyberQuizGrupp1.SHARED.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CyberQuizGrupp1.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        //röd markering på grund av att BLL inte implementerat ISubCategoryService än?
        public SubCategoriesController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        //get: api/categories/1/subcategories
        [Authorize] //kräver inloggning
        [HttpGet("{categoryId:int}/subcategories")]
        public async Task<ActionResult<List<SubCategoryDTO>>> GetSubCategories(int categoryId)
        {
            //snabb validering
            if (categoryId <= 0)
                return BadRequest("categoryid must be greater than 0");

            //hämta userid från jwt/cookie claims (diskuterades med Hassan?)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized("user not authenticated");

            //ingen affärslogik här - kalla bll
            var result = await _subCategoryService.GetSubCategoriesByCategoryAsync(categoryId, userId);

            return Ok(result); //result är redan List<SubCategoryDTO>, behöver inte .ToList()
        }
    }
}
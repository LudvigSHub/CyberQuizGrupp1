//using CyberQuizGrupp1.Services.Interfaces;

using CyberQuizGrupp1.BLL.Interfaces;
using CyberQuizGrupp1.SHARED.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CyberQuizGrupp1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        //röd markering på grund av att BLL inte implementerat ISubCategoryService än?
        public SubCategoriesController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        // GET: api/subcategories/{categoryId}
        //[Authorize] //kräver inloggning
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<List<SubCategoryDTO>>> GetSubCategories(int categoryId, [FromQuery] string userId)
        {
            //snabb validering
            if (categoryId <= 0)
                return BadRequest("categoryid must be greater than 0");

            //ingen affärslogik här - kalla bll
            var subCategories = await _subCategoryService.GetSubCategoriesByCategoryAsync(categoryId, userId);

            return Ok(subCategories); //result är redan List<SubCategoryDTO>, behöver inte .ToList()
        }
    }
}
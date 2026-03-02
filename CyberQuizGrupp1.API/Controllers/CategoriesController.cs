using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CyberQuizGrupp1.SHARED.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CyberQuizGrupp1.SHARED.DTOs;
using CyberQuizGrupp1.BLL.Interfaces;

namespace CyberQuizGrupp1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] //aktivera senare när identity är klar?
    public class CategoriesController : ControllerBase
    {
        //injicera service för business logik
        private readonly ICategoryService _categoryService;

        //tar emot ICategoryService via Dependency Injection och sparar den i _categoryService
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //get: api/categories
        //hämta alla kategorier
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

    }

}
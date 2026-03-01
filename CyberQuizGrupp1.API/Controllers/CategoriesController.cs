using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CyberQuizGrupp1.Services.Interfaces;
using CyberQuizGrupp1.SHARED.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        //get: api/categories/with-subcategories
        //hämta alla kategorier med subkategorier
        [HttpGet("with-subcategories")]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetAllWithSubCategories()
        {
            var categories = await _categoryService.GetAllCategoriesWithSubCategoriesAsync();
            return Ok(categories);
        }

        //get: api/categories/5
        //hämta en specifik kategori
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound($"category with id {id} not found");
            }

            return Ok(category);
        }

        //get: api/categories/5/with-subcategories
        //hämta en kategori med dess subkategorier
        [HttpGet("{id}/with-subcategories")]
        public async Task<ActionResult<CategoryModel>> GetByIdWithSubCategories(int id)
        {
            var category = await _categoryService.GetCategoryWithSubCategoriesAsync(id);

            if (category == null)
            {
                return NotFound($"category with id {id} not found");
            }

            return Ok(category);
        }

        //post: api/categories
        //skapa en ny kategori
        [HttpPost]
        public async Task<ActionResult<CategoryModel>> Create([FromBody] CreateCategoryRequest request)
        {
            try
            {
                var category = await _categoryService.CreateCategoryAsync(request.Name);
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        //put: api/categories/5
        //uppdatera en kategori
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateCategoryRequest request)
        {
            try
            {
                var success = await _categoryService.UpdateCategoryAsync(id, request.Name);

                if (!success)
                {
                    return NotFound($"category with id {id} not found");
                }

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //delete: api/categories/5
        //ta bort en kategori
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);

            if (!success)
            {
                return NotFound($"category with id {id} not found");
            }

            return NoContent();
        }
    }

    //enkla request dto:er direkt i samma fil (för enkelhetens skull)
    //om det blir mer komplexa request/response modeller eller
    //fler endpoints kan det vara bättre att flytta ut dem till
    //egna filer i en "DTOs" mapp eller liknande i API projektet

    //CategoryRequestDTO
    public record CreateCategoryRequest(string Name);
    public record UpdateCategoryRequest(string Name);
}
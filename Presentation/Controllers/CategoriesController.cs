using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IServiceManager _services;

        public CategoriesController(IServiceManager service)
        {
            _services=service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var category = await _services.CategoryService.GetAllCategoriesAsync(false);
            return Ok(category);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneCategory([FromRoute(Name = "id")]int id)
        {
            var category = await _services.CategoryService.GetOneCategoryByIdAsync(id, false);
            return Ok(category);
        }

        [HttpPost(Name = "CreateOneCategory")]
        public async Task<IActionResult> CreateOneCategory([FromBody]Category category)
        {
            var created = await _services.CategoryService.CreateOneCategoryAsync(category);
            return Ok(created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneCategoy([FromRoute(Name = "id")] int id,
            [FromBody] Category category)
        {
            await _services.CategoryService.UpdateOneCategoryAsync(id, category,true);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneCategory([FromRoute(Name = "id")]int id)
        {
            await _services.CategoryService.DeleteOneCategoryAsync(id, false);
            return NoContent();
        }
    }
}

using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Category> categories = _categoryRepository.GetAll();
            return Ok(categories);
        }
        [HttpGet("{id}", Name = "EmployeeDetailRoute")]
        public IActionResult Details([FromRoute] int id)
        {
            Category category = _categoryRepository.GetById(id);
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.AddNew(category);
                string url = Url.Link("EmployeeDetailRoute", new { id = category.Id });
                return Created(url,category);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Edit([FromRoute] int id, [FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(id, category);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _categoryRepository.Delete(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}

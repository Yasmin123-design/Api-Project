using E_Commerce.Dto;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.Contracts;

namespace E_Commerce.Controllers
{
    // postman => content-type application/json
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;
        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }
        [HttpGet]
        //[Authorize]
        public IActionResult Index()
        {
            List<Food> foods = _foodRepository.GetAll();
            return Ok(foods);
        }
        [HttpGet("{id}",Name ="detail")]
        public IActionResult Details(int id)
        {
            Food food = _foodRepository.GetById(id);
            return Ok(food);
        }
        [HttpPost]
        public IActionResult CreateNew(Food food)
        {
            if(ModelState.IsValid)
            {
                _foodRepository.AddNew(food);

                // المعامل الأول url هو الرابط(URL) الذي يشير إلى المورد الذي تم إنشاؤه(في هذه الحالة، رابط تفاصيل الموظف الجديد).
                // المعامل//المعامل الثاني employee هو كائن الموظف الذي تم إنشاؤه والذي سيتم تضمينه في جسم الاستجابة(Body)
                string url = Url.Link("detail", new {id=food.Id});
                return Created(url, food);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public IActionResult Edit([FromRoute]int id , [FromBody]Food food)
        {
            if(ModelState.IsValid)
            {
                _foodRepository.Update(id, food);
                return StatusCode(StatusCodes.Status204NoContent); // not content return  
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _foodRepository.Delete(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("dto/{id}")]
        public IActionResult FoodWithCategory(int id)
        {
            Food food = _foodRepository.GetWithCategoryName(id);
            FoodNameWithCategoryName product = new FoodNameWithCategoryName();
            product.FoodName = food.Name;
            product.CategoryName = food.Category.Name;
            return Ok(product);
        }
    }
}

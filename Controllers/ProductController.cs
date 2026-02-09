using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_E_Commerce_API.DTO;
using Mini_E_Commerce_API.Models;

namespace Mini_E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController(ApplicationContext context) : ControllerBase
    {

        private readonly ApplicationContext _context = context;


        [HttpGet]
        public IActionResult GetAll()
        {
            // 1. Retrieve data from the database (as Entity)
            var productsFromDb = _context.Products.ToList();

            // 2. Convert Entity to DTO (manual conversion as discussed)
            var productsDtoList = productsFromDb.Select(p => new ProductsDto
            {
                Id = p.Id,
                name = p.name,
                Price = p.Price,
                CatogryId = p.CatogryId
            }).ToList();

            // 3. Return the DTO to the user
            return Ok(productsDtoList);
        }

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var productFromDb = await _context.Products.FindAsync(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            var productDto = new ProductsDto
            {
                Id = productFromDb.Id,
                name = productFromDb.name,
                Price = productFromDb.Price,
                CatogryId = productFromDb.CatogryId

            };
            return Ok(productDto);
        }
        [HttpPost]
        // Added async and Task here
        public async Task<IActionResult> Create([FromBody] ProductsDto productDto)
        {
            var productEntity = new Product
            {
                name = productDto.name,
                Price = productDto.Price,
                CatogryId = productDto.CatogryId
            };

            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync(); // await requires the method to be async

            productDto.Id = productEntity.Id;

            var response = new GeneralResponse
            {
                IsSuccess = true,
                Message = "Product created successfully!",
                Data = productDto // We send the DTO here so the user sees what was added
            };

            return Ok(response);
        }
    }
    
}
    



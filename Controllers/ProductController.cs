using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // مهم عشان Async
using Mini_E_Commerce_API.DTO;
using Mini_E_Commerce_API.Models;

namespace Mini_E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     [Authorize] // مفعل عشان الأمان
    public class ProductController(ApplicationContext context) : ControllerBase
    {
        private readonly ApplicationContext _context = context;

        // 1. GET ALL: Async + AsNoTracking (Performance)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // تحسين الأداء: بنعمل Select جوه الداتا بيز عشان نرجع الأعمدة المطلوبة بس
            // AsNoTracking: بتسرع الاستعلام لأننا مش محتاجين نعدل الداتا دي
            var products = await _context.Products
                .AsNoTracking()
                .Select(p => new ProductsDto
                {
                    Id = p.Id,
                    name = p.name,       // يفضل تخليها Name (Capital) في الموديل مستقبلاً
                    Price = p.Price,
                    CatogryId = p.CatogryId // يفضل تعديلها لـ CategoryId مستقبلاً
                })
                .ToListAsync();

            return Ok(products);
        }

        // 2. GET BY ID: Added Route + Async
        [HttpGet("{id}")] // لازم نحدد الـ Route عشان يعرف ياخد الـ ID من الرابط
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            var productDto = new ProductsDto
            {
                Id = product.Id,
                name = product.name,
                Price = product.Price,
                CatogryId = product.CatogryId
            };

            return Ok(productDto);
        }

        // 3. CREATE: Validation + 201 Created Response
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductsDto productDto)
        {
            // التأكد إن البيانات مبعوثة صح حسب الـ Annotations اللي في الـ DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productEntity = new Product
            {
                name = productDto.name,
                Price = productDto.Price,
                CatogryId = productDto.CatogryId
            };

            await _context.Products.AddAsync(productEntity);
            await _context.SaveChangesAsync();

            // تحديث الـ ID في الـ DTO عشان يرجع لليوزر
            productDto.Id = productEntity.Id;

            // Best Practice: في الـ POST بنرجع 201 Created مش 200 OK
            // وبنرجع في الـ Header رابط للمنتج الجديد
            return CreatedAtAction(nameof(GetById), new { id = productEntity.Id }, new
            {
                IsSuccess = true,
                Message = "Product created successfully!",
                Data = productDto
            });
        }
    }
}
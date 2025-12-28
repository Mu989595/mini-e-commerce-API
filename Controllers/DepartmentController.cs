//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebAPIDotNet.DTO;
//using WebAPIDotNet.Models;

//namespace WebAPIDotNet.Controllers
//{
//    [Route("api/[controller]")] // fluent API 
//    [ApiController]
//    public class DepartmentController : ControllerBase
//    {

//        Context context;
//        public DepartmentController(Context _context)
//        {
//            context = _context;
//        }

//        [HttpPost]
//        public IActionResult CreateEmployee(DeptWithEmpCountDTO empDto)
//        {
//            // 1. التحقق من صحة البيانات (Validation)
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            // 2. تحويل الـ DTO إلى Entity (Mapping)
//            var newEmployee = new Employee
//            {
//                Id= empDto.Id,
//                Name = empDto.Name,
//                DepartmentId = empDto.DepartmentId
//            };

//            // 3. هنا نقوم بحفظ newEmployee في قاعدة البيانات عبر الـ Context
//            // _context.Employees.Add(newEmployee);
//            // _context.SaveChanges();

//            return Ok("تم إضافة الموظف بنجاح باستخدام الـ DTO");
//        }
//        [HttpGet]
//        public IActionResult DisplayAllDept()
//        {
//            List<Department> deptlist =
//                context.Department.Include(d => d.Emps).ToList();

//            return Ok(deptlist);
//        }
//        [HttpGet]
//        [Route("{id}")]
//        public IActionResult GetByID(int id)
//        {
//            Department dept
//                = context.Department.FirstOrDefault(d => d.Id == id);
//            return Ok(dept);
//        }

//        [HttpPost]
//        public IActionResult Adddept(Department dept)
//        {
//            context.Department.Add(dept);
//            context.SaveChanges();
//            return Created();
//        }
        
     
//    }
//}

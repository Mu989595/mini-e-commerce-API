//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using WebAPIDotNet.Models;

//namespace WebAPIDotNet.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BindingController : ControllerBase
//    {
//        [HttpGet("{age:int}/{name:alpha}")]
//        public IActionResult TestPrimitev(int age, string? name)
//        {
//            return Ok();
//        }
//        [HttpPost("{name}")]
//        public IActionResult testobj (int age, string? name)
//        {
//            return Ok();
//        }

//        [HttpGet("{id:int}/ {Name:alpha}/ {ManagerName:alpha}")]
//        public IActionResult testCustomeBind([FromRoute]Department dept)
//        {
//                       return Ok();
//        }
//    }
//}

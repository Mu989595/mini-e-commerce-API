using System.ComponentModel.DataAnnotations;

namespace Mini_E_Commerce_API.DTO
{
    public class ProductsDto
    {
        [Required(ErrorMessage = "id is requird")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }
        public int Price { get; set; }
        public int CatogryId { get; set; }
    }
}

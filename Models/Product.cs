using System.ComponentModel.DataAnnotations.Schema;

namespace Mini_E_Commerce_API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string name { get; set; }

        public int Price { get; set; }
        [ForeignKey("Catogry")]

        public int CatogryId { get; set; }

        }
}

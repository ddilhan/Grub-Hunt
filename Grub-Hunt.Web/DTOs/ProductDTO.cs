using System.ComponentModel.DataAnnotations;

namespace Grub_Hunt.Web.DTOs
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
    }
}

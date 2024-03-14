using System.ComponentModel.DataAnnotations;

namespace Grub_Hunt.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public string? DeletedBy { get; set; }

    }
}

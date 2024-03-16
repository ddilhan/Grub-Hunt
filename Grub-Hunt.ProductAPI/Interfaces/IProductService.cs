using Grub_Hunt.ProductAPI.DTOs;
using Grub_Hunt.ProductAPI.Models;

namespace Grub_Hunt.ProductAPI.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProducts();
        public Product? GetProduct(int id);
        public void CreateProduct(ProductDTO model);
        public void EditProduct(ProductDTO model);
        public void DeleteProduct(Product id);
    }
}

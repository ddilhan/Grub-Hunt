using AutoMapper;
using Grub_Hunt.ProductAPI.Data;
using Grub_Hunt.ProductAPI.DTOs;
using Grub_Hunt.ProductAPI.Interfaces;
using Grub_Hunt.ProductAPI.Models;

namespace Grub_Hunt.ProductAPI.Implementations
{
    public class ProductService : IProductService
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        public ProductService(AppDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _dbContext.Set<Product>().ToList();
        }

        public Product? GetProduct(int id)
        {
            return _dbContext.Set<Product>().FirstOrDefault(x => x.ID == id);
        }

        public void CreateProduct(ProductDTO model)
        {
            var product = _mapper.Map<Product>(model);
            product.CreatedDate = DateTime.Now;

            //get and assign the id from token
            product.CreatedBy = "";

            _dbContext.Set<Product>().Add(product);
            _dbContext.SaveChanges();
        }

        public void EditProduct(ProductDTO model)
        {
            var product = _dbContext.Set<Product>().FirstOrDefault(x => x.ID == model.ID);

            if (product is not null)
            {
                product.Name = model.Name;
                product.ModifiedDate = DateTime.Now;
                product.ModifiedBy = "";

                _dbContext.Set<Product>().Update(product);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteProduct(Product product)
        {
            product.DeletedBy = "";
            product.DeletedDate = DateTime.Now;

            _dbContext.Set<Product>().Update(product);
            _dbContext.SaveChanges();
        }
    }
}

using AutoMapper;
using Grub_Hunt.ProductAPI.Data;
using Grub_Hunt.ProductAPI.DTOs;
using Grub_Hunt.ProductAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grub_Hunt.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly AppDBContext _dbContext;
        private ResponseDTO _responseDTO;
        private readonly IMapper _mapper;

        public ProductController(AppDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _responseDTO = new();
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetProducts")]
        public ResponseDTO GetProducts()
        {
            try
            {
                var products = _dbContext.Set<Product>().ToList();

                _responseDTO.Result = products;
                _responseDTO.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.Success = false;
                _responseDTO.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return _responseDTO;
        }

        //[Authorize]
        [HttpGet("GetProduct{id:int}")]
        public ResponseDTO GetProduct(int id)
        {
            try
            {
                var product = _dbContext.Set<Product>().FirstOrDefault(x => x.ID == id);

                _responseDTO.Result = product;
                _responseDTO.StatusCode = System.Net.HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.Success = false;
                _responseDTO.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return _responseDTO;
        }

        //[Authorize("ADMIN")]
        [HttpPost("CreateProduct")]
        public ResponseDTO CreateProduct([FromBody] ProductDTO model)
        {
            try
            {
                if (model != null) 
                {
                    var product = _mapper.Map<Product>(model);
                    product.CreatedDate = DateTime.Now;

                    //get and assign the id from token
                    product.CreatedBy = "";

                    _dbContext.Set<Product>().Add(product);
                    _dbContext.SaveChanges();

                    _responseDTO.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    _responseDTO.StatusCode=System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.Success = false;
                _responseDTO.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return _responseDTO;
        }

        //[Authorize("ADMIN")]
        [HttpPost("EditProduct")]
        public ResponseDTO EditProduct([FromBody] ProductDTO model)
        {
            try
            {
                if (model is not null && model.ID is not 0)
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

                    _responseDTO.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    _responseDTO.StatusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.Success = false;
                _responseDTO.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return _responseDTO;
        }

        //[Authorize("ADMIN")]
        [HttpPost("DeleteProduct")]
        public ResponseDTO DeleteProduct(int id)
        {
            try
            {
                var product = _dbContext.Set<Product>().FirstOrDefault(x => x.ID == id);

                if (product is not null)
                {
                    product.DeletedBy = "";
                    product.DeletedDate = DateTime.Now;

                    _dbContext.Set<Product>().Update(product);
                    _dbContext.SaveChanges();

                    _responseDTO.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    _responseDTO.StatusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.Success = false;
                _responseDTO.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return _responseDTO;
        }
    }
}

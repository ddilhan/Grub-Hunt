using AutoMapper;
using Grub_Hunt.ProductAPI.DTOs;
using Grub_Hunt.ProductAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grub_Hunt.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {=
        private ResponseDTO _responseDTO;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IMapper mapper, IProductService productService)
        {
            _responseDTO = new();
            _mapper = mapper;
            _productService = productService;
        }

        [Authorize]
        [HttpGet("GetProducts")]
        public ResponseDTO GetProducts()
        {
            try
            {
                var products = _productService.GetProducts();

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
                var product = _productService.GetProduct(id);

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
                    _productService.CreateProduct(model);
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
        [HttpPost("EditProduct")]
        public ResponseDTO EditProduct([FromBody] ProductDTO model)
        {
            try
            {
                if (model is not null && model.ID is not 0)
                {
                    _productService.EditProduct(model);
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
                var product = _productService.GetProduct(id);

                if (product is not null)
                {
                    _productService.DeleteProduct(product);
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

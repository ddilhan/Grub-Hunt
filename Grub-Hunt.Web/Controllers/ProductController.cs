using Grub_Hunt.Web.DTOs;
using Grub_Hunt.Web.Interfaces;
using Grub_Hunt.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Grub_Hunt.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IBaseService _baseService;
        private readonly ServiceUrls _serviceUrls;
        private readonly ConvertObjects _converter;

        public ProductController(IBaseService baseService, IOptions<ServiceUrls> options, ConvertObjects converter)
        {
            _baseService = baseService;
            _serviceUrls = options.Value;
            _converter = converter;
        }
        public async Task<IActionResult> Index()
        {
            var model = new List<ProductDTO>();
            try
            {
                var response = await _baseService.SendAsync(new()
                {
                    HttpMethod = HttpMethod.Get,
                    Url = $"{_serviceUrls.ProductAPIBaseUrl}/api/Product/GetProducts"
                }, true);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ViewBag.GeneralError = response.Message;
                }
                else
                {
                    model = _converter.JsonStringToType<List<ProductDTO>>(response.Result.ToString());
                }
            }
            catch (Exception e)
            {

            }
            return View(model);
        }
    }
}

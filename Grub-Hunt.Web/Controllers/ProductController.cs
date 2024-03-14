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
        public ProductController(IBaseService baseService, IOptions<ServiceUrls> options)
        {
            _baseService = baseService;
            _serviceUrls = options.Value;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _baseService.SendAsync(new() 
            {
                HttpMethod = HttpMethod.Get, 
                Url = $"{_serviceUrls.ProductAPIBaseUrl}/api/Product/GetProducts" 
            }, true);

            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {

        private IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {


            //// 1. YOL
            //if (String.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            //{
            //    _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            //}

            // 2. YOL
            if(!_memoryCache.TryGetValue("zaman",out string zamanCache))
            {
                
                
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                options.SlidingExpiration = TimeSpan.FromSeconds(10);



                _memoryCache.Set<string>("zaman", DateTime.Now.ToString(),options);
            }







            return View();
        }

        public IActionResult Show()
        {

            ////_memoryCache.Remove("zaman");

            //_memoryCache.GetOrCreate<string>("zaman", entry =>
            //{
            //    return DateTime.Now.ToString();
            //});

            //ViewBag.zaman = _memoryCache.Get<string>("zaman");


            _memoryCache.TryGetValue("zaman", out string zamanCache);
            ViewBag.zaman = zamanCache;


            return View();

        }


    }
}

﻿using InMemoryApp.Web.Models;
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
            if (!_memoryCache.TryGetValue("zaman", out string zamanCache))
            {

            }
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddMinutes(5);


            options.SlidingExpiration = TimeSpan.FromSeconds(10);
            options.Priority = CacheItemPriority.Low;

            options.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                _memoryCache.Set("callback", $"{key}->{value} => sebep:{reason}");
            });

            _memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);

            Product p = new Product { Id = 1, Name = "Kalem", Price = 100 };

            _memoryCache.Set<Product>("product:1", p);

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
            _memoryCache.TryGetValue("callback", out string callback);
            ViewBag.zaman = zamanCache;
            ViewBag.callback = callback;

            ViewBag.product = _memoryCache.Get<Product>("product:1");


            return View();

        }


    }
}

using DB.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ui.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> db;
        private readonly IRepository<Category> catDb;

        public ProductController(IRepository<Product> _db, IRepository<Category> _catDb)
        {
            db = _db;
            catDb = _catDb;
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ProductAdd([FromBody] Product entitiy)
        {
            try
            {
                entitiy.CategoryName = catDb.Where(x => x.Idcategory == entitiy.CategoryId).FirstOrDefault().Name;
                db.Add(entitiy);

                return Json(new { success = true, message = "Product başarıyla eklendi." });
            }
            catch
            {
                return Json(new { error = true, message = "Bir hata oluştu lütfen daha sonra tekrar deneyin !" });
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ProductList()
        {
            return Json(new
            {
                data = db.All().ToList()
                .Select(p => new { p.CategoryName, p.Name, p.Price }).ToList()
                .Select(x => new
                {
                    cat = x.CategoryName,
                    nam = x.Name,
                    pri = x.Price + " $"
                }).ToList()
            });
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult CategoryAdd([FromBody] Category entitiy)
        {
            try
            {
                catDb.Add(entitiy);
                return Json(new { success = true, message = "Category başarıyla eklendi." });
            }
            catch
            {
                return Json(new { error = true, message = "Bir hata oluştu lütfen daha sonra tekrar deneyin !" });
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult CategorytList()
        {
            return Json(new
            {
                data = catDb.All().ToList()
                .Select(c => new { c.Name, c.Idcategory }).ToList()
                .Select(x => new
                {
                    nam = x.Name,
                    id = x.Idcategory
                }).ToList()
            });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MVC_CRUD.DataContext;
using MVC_CRUD.Models;

namespace MVC_CRUD.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ApplicationDBContext _db;

        public CategoryController(ApplicationDBContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Category.ToList();
            return View(categories);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name & Display Order cannot be same");
            }
            if (ModelState.IsValid)
            {
                _db.Category.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET - EDIT
        public IActionResult Edit(int id)
        {
            Category category = _db.Category.Find(id);
            //Category category = _db.Category.FirstOrDefault(x => x.Id == id);
            //Category category = _db.Category.SingleOrDefault(x => x.Id == id);
            //Category category = _db.Category.Where(x => x.Id == id).FirstOrDefault();

            return View(category);
        }

        //POST - EDIT
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name & Display Order cannot be same");
            }
            if (ModelState.IsValid)
            {
                _db.Category.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET - DELETE
        public IActionResult Delete(int id)
        {
            Category category = _db.Category.Find(id);
            //Category category = _db.Category.FirstOrDefault(x => x.Id == id);
            //Category category = _db.Category.SingleOrDefault(x => x.Id == id);
            //Category category = _db.Category.Where(x => x.Id == id).FirstOrDefault();

            return View(category);
        }

        //POST - DELETE
        [HttpPost(Name ="Delete")]
        public IActionResult Delete(Category category)
        {
            //Category categoryToRemove = _db.Category.Find(Category.Id);
            _db.Category.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

        ////POST - DELETE
        //[HttpPost(Name = "Delete")]
        //public IActionResult Delete(Category category)
        //{
        //    //Category categoryToRemove = _db.Category.Find(id);
        //    _db.Category.Remove(category);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}

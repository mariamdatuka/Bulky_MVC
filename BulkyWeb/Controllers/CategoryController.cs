﻿using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List <Category> objCategoryList=_db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
		public IActionResult Create(Category obj)
		{
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DislayOrder cannot match the Name");
            }
            if (ModelState.IsValid) {
				_db.Categories.Add(obj);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}

            return View();
		}
		public IActionResult Edit(int? id)
		{
			if(id==null || id == 0)
			{
				return NotFound();
			}
			Category categoryFromDb=_db.Categories.FirstOrDefault(c => c.Id == id);	
			if(categoryFromDb==null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The DislayOrder cannot match the Name");
			}
			if (ModelState.IsValid)
			{
				_db.Categories.Add(obj);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View();
		}
	}
}

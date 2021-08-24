using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StellarisPlanetList.Data;
using StellarisPlanetList.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StellarisPlanetList.Controllers
{
    public class PlanetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// How mnay parks per page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? id)
        {
            int pageNum = id ?? 1;
            const int PageSize = 20;
            ViewData["CurrentPage"] = pageNum;

            int numProducts = await PlanetDB.GetTotalProductsAsync(_context);
            int totalPages = (int)Math.Ceiling((double)numProducts / PageSize);
            ViewData["MaxPage"] = totalPages;

            List<PlanetViewModel> products = await PlanetDB.GetProductsAsync(_context, PageSize, pageNum);



            return View(products);
        }

        /// <summary>
        ///  adds a new park but doesnt work as whos going to make a natinal park
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PlanetViewModel p)
        {
            if (ModelState.IsValid)
            {
                p = await PlanetDB.AddProductAsync(_context, p);

                TempData["Message"] = $"{p.PlanetName} was added successfully";


                return RedirectToAction("Index");
            }

            return View();
        }

        // The following actions are for admins only and have not been implemented yet

        /// <summary>
        /// lets you edit a 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            PlanetViewModel p = await PlanetDB.GetProductAsync(_context, id);
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PlanetViewModel p)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(p).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                ViewData["Message"] = "Product updated successfully";
            }

            return View(p);
        }

        /// <summary>
        /// lets you delete a park, if needed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            PlanetViewModel p = await PlanetDB.GetProductAsync(_context, id);

            return View(p);
        }

        /// <summary>
        /// just conforms you want that park deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            PlanetViewModel p = await PlanetDB.GetProductAsync(_context, id);

            _context.Entry(p).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            TempData["Message"] = $"{p.PlanetName} was deleted";

            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Film_Management_System_API;
using Film_Management_System_API.Models;
using AutoMapper;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using Film_Management_System_API.DataModels;

namespace Film_Management_System_MVC.Controllers
{
    public class FilmsController : Controller
    {
        private readonly MoviesContext _context;
        private static string _name;
        private IConfiguration configuration;
       
        public FilmsController(MoviesContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        // GET: Films
        public async Task<IActionResult> Index()
        {
            var moviesContext = _context.Films.Include(f => f.Actor).Include(f => f.Category).Include(f => f.Language).Include(f => f.OriginalLanguage);
            return View(await moviesContext.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Search(string name)
        {
            if ( name == null || _context.Films == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.Actor)
                .Include(f => f.Category)
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .FirstOrDefaultAsync(m => m.Title == name);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
           
             if (id == null || _context.Films == null)
             {
                 return NotFound();
             }

             var film = await _context.Films
                 .Include(f => f.Actor)
                 .Include(f => f.Category)
                 .Include(f => f.Language)
                 .Include(f => f.OriginalLanguage)
                 .FirstOrDefaultAsync(m => m.FilmId == id);
             if (film == null)
             {
                 return NotFound();
             }

             return View(film);
        }

/*        public IActionResult SearchByName()
        
        {
            Film f = new Film();
            return View();
        }*/

     /*   [HttpPost, ActionName("SearchByName")]
               [ValidateAntiForgeryToken]
               public async Task<IActionResult> SearchByName(IFormCollection collection)

               {
                   string name = collection["Title"];
                   var model = await this.SendDataToApi<string, Film>(
                      baseUri: configuration.GetConnectionString("FilmsUri"),
                      requestUrl: "api/Films/SearchByName/",name
                      );
                   var credstring = JsonConvert.SerializeObject(model);
                   TempData["cred"] = credstring;
                   return RedirectToAction("ViewMovie", "Films");

               }*/
       /* [HttpPost]
        public ActionResult SearchByName(string name)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5076/api/Films/Name");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Film>("student", Film);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View();
        }*/
        public IActionResult ViewMovie()
        {
            var credstring = TempData["cred"].ToString();
            var cred = JsonConvert.DeserializeObject<IEnumerable<Film>>(credstring);

            return View(model: cred);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "FirstName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "Name");
            ViewData["OriginalLanguageId"] = new SelectList(_context.Languages, "LanguageId", "Name");
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public async Task<IActionResult> Create([Bind("FilmId,Description,Title,LanguageId,OriginalLanguageId,Length,ReplacementCost,Rating,SpecialFeatures,ActorId,CategoryId,ReleaseYear,RentalDuration")] Film film)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "FirstName", film.ActorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", film.CategoryId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "Name", film.LanguageId);
            ViewData["OriginalLanguageId"] = new SelectList(_context.Languages, "LanguageId", "Name", film.OriginalLanguageId);
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Films == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "FirstName", film.ActorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", film.CategoryId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "Name", film.LanguageId);
            ViewData["OriginalLanguageId"] = new SelectList(_context.Languages, "LanguageId", "Name", film.OriginalLanguageId);
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public async Task<IActionResult> Edit(decimal id, [Bind("FilmId,Description,Title,LanguageId,OriginalLanguageId,Length,ReplacementCost,Rating,SpecialFeatures,ActorId,CategoryId,ReleaseYear,RentalDuration")] Film film)
        {
            if (id != film.FilmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.FilmId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "FirstName", film.ActorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", film.CategoryId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "Name", film.LanguageId);
            ViewData["OriginalLanguageId"] = new SelectList(_context.Languages, "LanguageId", "Name", film.OriginalLanguageId);
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Films == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.Actor)
                .Include(f => f.Category)
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .FirstOrDefaultAsync(m => m.FilmId == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Films == null)
            {
                return Problem("Entity set 'MoviesContext.Films'  is null.");
            }
            var film = await _context.Films.FindAsync(id);
            if (film != null)
            {
                _context.Films.Remove(film);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(decimal id)
        {
          return (_context.Films?.Any(e => e.FilmId == id)).GetValueOrDefault();
        }
    }
}

using Film_Management_System_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Film_Management_System_MVC.Controllers
{
    public class AdmController : Controller
    {
        private readonly MoviesContext _context;
        private static string _name;
        private IConfiguration configuration;

        public AdmController(MoviesContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FilmIndex()
        {
            return View();
        }
        public IActionResult Index1()
        {
            var moviesContext = _context.Films.Include(f => f.Actor).Include(f => f.Category).Include(f => f.Language).Include(f => f.OriginalLanguage);
            return View(moviesContext.ToList());
        }
    }
    }


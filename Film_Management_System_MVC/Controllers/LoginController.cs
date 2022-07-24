using Film_Management_System_API.DataModels;
using Film_Management_System_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using System.Text;

namespace Film_Management_System_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly MoviesContext moviesContext;
        private readonly IConfiguration configuration;
        string URLFilms = @"http://localhost:5076/api/Auth/Login";
        public LoginController(MoviesContext moviesContext, IConfiguration configuration)
        {
            this.moviesContext = moviesContext;
            this.configuration = configuration;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(IFormCollection collection)
        {
            string msg = "";

            Admin a = new Admin();
            a.AdminUsernameEmail = collection["AdminUsernameEmail"];
            a.AdminPassword = collection["AdminPassword"];
            using (var client = new HttpClient())
            {
                var query = from d in moviesContext.Admins
                            where d.AdminUsernameEmail == a.AdminUsernameEmail && d.AdminPassword == a.AdminPassword
                            select new Admin(){
                                AdminUsernameEmail=d.AdminUsernameEmail,
                                AdminPassword=d.AdminPassword};
                List<Admin> k = query.ToList();

                foreach (var admin in k)
                {

                    if (a.AdminUsernameEmail == admin.AdminUsernameEmail && a.AdminPassword == admin.AdminPassword)
                    {
                        return RedirectToAction("Index", "Adm");
                    }



                }

                return View();

            }


        }
        public IActionResult Search()

        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Search(IFormCollection collection)
        {

            string Name = collection["Title"];

            using (var client = new HttpClient())
            {
                var query = from d in moviesContext.Films
                            where d.Title == Name
                            select new Film()
                            {
                                Title = d.Title,
                                ReleaseYear = d.ReleaseYear,
                                Rating = d.Rating,
                            };
                List<Film> k = query.ToList();
                foreach (var film in k)
                {
                    if (film.Title == Name)
                    {
                        return RedirectToAction("Index", "Films");
                    }
                }
                return View();


            }
        }

    }
}
    

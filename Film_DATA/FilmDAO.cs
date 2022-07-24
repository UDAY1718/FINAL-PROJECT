
using Film_Management_System_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Films_DataAccess
{
    public class FilmDAO
    {
        MoviesContext _moviesContext;
        public FilmDAO(MoviesContext context) => _moviesContext = context;
        public Film GetFilmByTitle(string t)
        {

            var query =
   from f in _moviesContext.Films
   join n in _moviesContext.Languages on f.LanguageId equals n.LanguageId
   where n.Name == t
   select new { f.Title, f.Rating, f.ReleaseYear };
            return (Film)query;
        }
    }
}

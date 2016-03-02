using MovieManagement.Models.DB_models;
using MovieManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieManagement.DAL
{
    public interface IRepository 
    {
         int PostMovies(MovieDTO newMovie);
         ICollection<Genres> FetchGenres(ICollection<int> genreIds);
    }
}
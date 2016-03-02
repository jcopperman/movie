using MovieManagement.Models.DB_models;
using MovieManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace MovieManagement.DAL
{
    public class MoviesRepository : IRepository
    {
        public int PostMovies(MovieDTO movie)
        {
            // Create Movie type object to add
            var newMovie = new Movies()
            {
                Title = movie.Title,
                Released = movie.Released
            };

            using (var db = new MoviesContext())
            {
                // Link genres from DB to the new Movie
                newMovie.Genres = new List<Genres>();
                foreach (var g in movie.GenreIds)
                {
                    var genreToAdd = db.Genres.FirstOrDefault(x => x.GenreId == g);
                    newMovie.Genres.Add(genreToAdd);
                }
                

                // Try to insert the new Movie

                db.Movies.Add(newMovie);
                db.SaveChanges();

                return newMovie.MovieId;
            }
        }


        // Ignore this for now
        public ICollection<Genres> FetchGenres(ICollection<int> genreIds)
        {
            var returnList = new List<Genres>();

            using (var db = new MoviesContext())
            {
               
            }

            return returnList;
        }
    }
}
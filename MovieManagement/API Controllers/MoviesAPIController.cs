using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MovieManagement.DAL;
using MovieManagement.Models.DB_models;
using MovieManagement.Models.DTO;
using System.Data.Entity.Validation;

namespace MovieManagement.API_Controllers
{
    //[RoutePrefix("api/movies")]
    public class MoviesAPIController : ApiController
    {

        [ResponseType(typeof(List<object>))]
        [Route("api/movies/get")]
        public IHttpActionResult GetMovies()
        {
            using (var db = new MoviesContext())
            {
                var movies = db.Movies.ToList();
                if (movies == null)
                {
                    return NotFound();
                }

                // Create generic object to get past the EF relationship serialization issues
                var movieListObj = new List<object>();

                foreach (var m in movies)
                {
                    //create a generic movie object for each Movie type from DB
                    var movieObj = new
                    {
                        id = m.MovieId,
                        title = m.Title,
                        released = m.Released,
                        genres = new List<object>()
                    };

                    // Add the generic genre list from the Movie 
                    // IF the movie has associated genres
                    foreach (var g in m.Genres)
                    {
                        movieObj.genres.Add(new
                        {
                            id = g.GenreId,
                            name = g.Name
                        });
                    }

                    // Add the generic movie object to the generic movie list
                    movieListObj.Add(movieObj);

                }

                // Return the created generic object list
                return Ok(movieListObj);
            }
        }



        // POST: api/MoviesAPI
        [ResponseType(typeof(MovieDTO))]
        [Route("api/movies/post")]
        public IHttpActionResult PostMovies(MovieDTO movie)
        {

            // Validation
            if (String.IsNullOrWhiteSpace(movie.Title))
            {
                return Ok("Movie Title is required");
            }

            if (movie.GenreIds == null || movie.GenreIds.Count == 0)
            {
                return Ok("A new Movie requires at least one genre to be selected");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // /Validation

            var repo = new MoviesRepository();
            var insertedMovieId = repo.PostMovies(movie);

            // Return the newly created movie
            return Ok(insertedMovieId);
        }
    }

}

using MovieManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MovieManagement.API_Controllers
{
    public class GenresAPIController : ApiController
    {
        [ResponseType(typeof(List<object>))]
        [Route("api/genres/get")]
        public IHttpActionResult GetGenres()
        {
            using (var db = new MoviesContext())
            {
                var genres = db.Genres.ToList();
                if (genres == null)
                {
                    return NotFound();
                }

                // Create generic object to get past the EF relationship serialization issues
                var genresListObj = new List<object>();

                foreach (var g in genres)
                {
                    //create a generic genre object for each Genres type from DB
                    var genreObj = new
                    {
                        id = g.GenreId,
                        name = g.Name
                    };                  

                    // Add the generic movie object to the generic movie list
                    genresListObj.Add(genreObj);

                }

                // Return the created generic object list
                return Ok(genresListObj);
            }
        }
    }
}
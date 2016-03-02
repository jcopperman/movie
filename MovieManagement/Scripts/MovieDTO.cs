using MovieManagement.Models.DB_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieManagement.Models.DTO
{   
    public class MovieDTO
    {
        public string Title { get; set; }
        public Nullable<DateTime> Released { get; set; }
        public ICollection<int> GenreIds { get; set; }
    }
}
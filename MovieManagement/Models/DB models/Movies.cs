using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieManagement.Models.DB_models
{
    public class Movies
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }

        [Required]
        public string Title { get; set; }        
               
        public Nullable<DateTime> Released { get; set; }

        public virtual ICollection<Genres> Genres { get; set; }
    }
}
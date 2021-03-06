using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Comment : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Body")]
        [Required(ErrorMessage = "Body is required")]
        public string Body { get; set; }

        public int MovieId  { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }
    }
}

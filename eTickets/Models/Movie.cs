using eTickets.Data;
using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Movie:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }

        //Profile
        public int ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }

        //Director
        public int DirectorId { get; set; }
        [ForeignKey("DirectorId")]
        public Director Director { get; set; }

        public List<Comment> Comments { get; set; }

    }
}

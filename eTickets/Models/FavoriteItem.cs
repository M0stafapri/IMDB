using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class FavoriteItem
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }

        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        public int FavoritesId { get; set; }
        [ForeignKey("FavoritesId")]
        public Favorite Favorite { get; set; }
    }
}

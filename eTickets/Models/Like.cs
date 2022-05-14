using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Like
    {
        [Key]
        public String Id { get; set; }

        [Display(Name = "Body")]
        [Required(ErrorMessage = "Body is required")]
        public string Type { get; set; }

        public int LikeId { get; set; }
        [ForeignKey("LikeId")]

        //Relationships
        public Movie Movies { get; set; }
    }
}

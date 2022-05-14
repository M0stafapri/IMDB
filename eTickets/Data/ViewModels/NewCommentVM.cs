using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.ViewModels
{
    public class NewCommentVM
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Comment is required")]
        public string Body { get; set; }

        //Comment
        //public string MovieId  { get; set; }
        public int MovieId = 4;
    }
}

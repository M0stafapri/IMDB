using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            Directors = new List<Director>();
            Profiles = new List<Profile>();
            Actors = new List<Actor>();
        }

        public List<Director> Directors { get; set; }
        public List<Profile> Profiles { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Movie> Movies { get; set; }

    }
}

using eTickets.Data.Base;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class ProfilesService:EntityBaseRepository<Profile>, IProfilesService
    {
        public ProfilesService(AppDbContext context) : base(context)
        {
        }
    }
}

using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IFavoritesService
    {
        Task StoreFavoritesAsync(List<FavoriteListItem> items, string userId, string userEmailAddress);
        Task<List<Favorite>> GetFavoritesByUserIdAndRoleAsync(string userId, string userRole);
    }
}

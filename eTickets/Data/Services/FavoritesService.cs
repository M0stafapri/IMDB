using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly AppDbContext _context;
        public FavoritesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Favorite>> GetFavoritesByUserIdAndRoleAsync(string userId, string userRole)
        {
            var favorites = await _context.Favorites.Include(n => n.FavoriteItems).ThenInclude(n => n.Movie).Include(n => n.User).ToListAsync();

            if(userRole != "Admin")
            {
                favorites = favorites.Where(n => n.UserId == userId).ToList();
            }

            return favorites;
        }

        public async Task StoreFavoriteAsync(List<FavoriteListItem> items, string userId, string userEmailAddress)
        {
            var favorite = new Favorite()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Favorites.AddAsync(favorite);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var favoritesItem = new FavoriteItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    FavoritesId = favorite.Id,
                };
                await _context.FavoriteItems.AddAsync(favoritesItem);
            }
            await _context.SaveChangesAsync();
        }

        Task IFavoritesService.StoreFavoritesAsync(List<FavoriteListItem> items, string userId, string userEmailAddress)
        {
            throw new NotImplementedException();
        }
    }
}

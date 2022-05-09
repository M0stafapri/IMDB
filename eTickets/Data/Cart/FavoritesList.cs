using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Data.Cart
{
    public class FavoritesList
    {
        public AppDbContext _context { get; set; }

        public string FavoritesListId { get; set; }
        public List<FavoriteListItem> FavoritesListItems { get; set; }

        [Display(Name = "Movie name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public FavoritesList(AppDbContext context)
        {
            _context = context;
        }

        public static FavoritesList GetFavoritesList(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new FavoritesList(context) { FavoritesListId = cartId };
        }

        public void AddItemToCart(Movie movie)
        {
            var favoriteListItem = _context.FavoritesListItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.FavoritesListId == FavoritesListId);

            if(favoriteListItem == null)
            {
                favoriteListItem = new FavoriteListItem()
                {
                    FavoritesListId = FavoritesListId,
                    Movie = movie,
                    Amount = 1
                };

                _context.FavoritesListItems.Add(favoriteListItem);
            } else
            {
                favoriteListItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Movie movie)
        {
            var favoriteListItem = _context.FavoritesListItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.FavoritesListId == FavoritesListId);

            if (favoriteListItem != null)
            {
                if(favoriteListItem.Amount > 1)
                {
                    favoriteListItem.Amount--;
                } else
                {
                    _context.FavoritesListItems.Remove(favoriteListItem);
                }
            }
            _context.SaveChanges();
        }
        //COPY TO DIRECTORS ACTORS ETC 
        public List<FavoriteListItem> GetFavoritesListItems()
        {
            return FavoritesListItems ?? (FavoritesListItems = _context.FavoritesListItems.Where(n => n.FavoritesListId == FavoritesListId).Include(n => n.Movie).ToList());
        }


        public async Task ClearFavoritesListAsync()
        {
            var items = await _context.FavoritesListItems.Where(n => n.FavoritesListId == FavoritesListId).ToListAsync();
            _context.FavoritesListItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}

using eTickets.Data.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.ViewComponents
{
    public class FavoritesListSummary : ViewComponent
    {
        private readonly FavoritesList _favoritesList;
        public FavoritesListSummary(FavoritesList favoritesList)
        {
            _favoritesList = favoritesList;
        }

        public IViewComponentResult Invoke()
        {
            var items = _favoritesList.GetFavoritesListItems();

            return View(items.Count);
        }
    }
}
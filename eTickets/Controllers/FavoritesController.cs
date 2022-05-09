using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize] 
    public class FavoritesController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly FavoritesList _favoritesList;
        private readonly IFavoritesService _favoritesService;

        public FavoritesController(IMoviesService moviesService, FavoritesList favoritesList, IFavoritesService favoritesService)
        {
            _moviesService = moviesService;
            _favoritesList = favoritesList;
            _favoritesService = favoritesService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var favorites = await _favoritesService.GetFavoritesByUserIdAndRoleAsync(userId, userRole);
            return View(favorites);
        }

        public IActionResult FavoritesList()
        {
            var items = _favoritesList.GetFavoritesListItems();
            _favoritesList.FavoritesListItems = items;

            var response = new FavoritesListVM()
            {
                FavoritesList = _favoritesList
            };

            return View(response);
        }

        public async Task<IActionResult> AddItemToFavoritesList(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _favoritesList.AddItemToCart(item);
            }
            return RedirectToAction(nameof(FavoritesList));
        }

        public async Task<IActionResult> RemoveItemFromFavoritesList(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _favoritesList.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(FavoritesList));
        }

        public async Task<IActionResult> CompleteFavorite()
        {
            var items = _favoritesList.GetFavoritesListItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _favoritesService.StoreFavoritesAsync(items, userId, userEmailAddress);
            await _favoritesList.ClearFavoritesListAsync();

            return View("FavoriteCompleted");
        }
    }
}

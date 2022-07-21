using Assist.July._2022.BE2.Application.Dtos.FavoriteDtos;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Assist.Jully._2022.BE2.Controllers
{
    [ApiController, Route("/api/[controller]"), Produces("application/json")]
    public class FavoriteController : Controller
    {
        private IFavoriteService favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            this.favoriteService = favoriteService;
        }

        [HttpPost("addToFavorites")]
        public async Task<ActionResult<FavoriteDto>> SaveFavoriteItem(FavoriteDto request)
        {
            try
            {
                var response = await favoriteService.PostAsync(request);

                if (response == null)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFavorites(Guid userId)
        {
            try
            {
                var response = await favoriteService.GetAsync(userId);

                if (response == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpDelete("{favoriteId}")]
        public async Task<IActionResult> DeleteListingFromFavorites(Guid favoriteId)
        {
            try
            {
                await favoriteService.DeleteAsync(favoriteId);

                return Ok("The listing has been removed from favorites!");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpDelete("{userId},{listingId}")]
        public async Task<IActionResult> DeleteListingFromFavorites(Guid userId, Guid listingId)
        {
            try
            {
                await favoriteService.DeleteByUserAndListingIdAsync(userId, listingId);

                return Ok("The listing has been removed from favorites!");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }
    }
}

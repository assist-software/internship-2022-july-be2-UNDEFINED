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
        //private readonly ApplicationDbContext applicationDbContext;

        public FavoriteController(IFavoriteService favoriteService/*, ApplicationDbContext applicationDbContext*/)
        {
            this.favoriteService = favoriteService;
            //this.applicationDbContext = applicationDbContext;
        }

        [HttpPost("addToFavorites")]
        public async Task<IActionResult> SaveFavoriteItem(PostFavoriteDto request)
        {
            try
            {
                await favoriteService.PostAsync(request);

                return Ok("Listing has been favored!");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        //[HttpPost("{userId},{listingId}")]//-----
        //public async Task<IActionResult> SaveFavoriteItemUsingId(Guid userId, Guid listingId)
        //{
        //    try
        //    {
        //        //await favoriteService.PostAsync(request);

        //        return Ok("Listing has been favored!");
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest("An error has occured");
        //    }
        //}

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFavorites(Guid userId)
        {
            try
            {
                var response = await favoriteService.GetAsync(userId);

                if(response == null)
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
        public async Task<IActionResult> DeleteListingFromFavorites(Guid id)
        {
            try
            {
                var response = await favoriteService.DeleteAsync(id);

                if (response == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok("The listing has been removed from favorites!");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }
    }
}

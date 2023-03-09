using BE.Core.Interfaces;
using BE.Enums;
using BE.Presentation.API.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Presentation.API.Controllers
{
    [Authorize]
    public class MoviesController : BaseController
    {
        private readonly IMovieServices _movieServices;
        public MoviesController(IMovieServices movieServices)
        {
            _movieServices = movieServices;
        }

        [HttpGet("{pageIndex:int}")]
        public async Task<IActionResult> GetMoviesByPage([FromRoute]int pageIndex)
        {
            var result = await _movieServices.GetMovies(new Guid(UserId), pageIndex);
            return Ok(result);
        }

        [HttpPut("{id:guid}/{actionType:int}")]
        public async Task<IActionResult> ModifyLikeNoForMovieById([FromRoute] Guid id, [FromRoute] ActionOfLikeEnum actionType)
        {
            var result = await _movieServices.ModifyLikeNo(id, new Guid(UserId), actionType);
            return Ok(result);
        }
    }
}

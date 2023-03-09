using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BE.Presentation.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public string AccountId { get { return HttpContext.User.FindFirst("Id") != null ? HttpContext.User.FindFirst("Id").Value : Guid.Empty.ToString(); } }
        public string UserId { get { return HttpContext.User.FindFirst("UserId") != null ? HttpContext.User.FindFirst("UserId").Value : Guid.Empty.ToString(); } }
        public BaseController()
        {

        }

        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            return base.Ok(value);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace HeulenderWolfAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class RootController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello from the Root controller!";
        }
    }
}

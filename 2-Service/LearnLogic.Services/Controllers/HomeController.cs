using LearnLogic.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;

namespace LearnLogic.Services.Controllers
{
    [ApiController]
    public class HomeController : BaseCQRSController
    {
        private readonly IHomeApplication _service;

        public HomeController(IHomeApplication service)
        {
            _service = service;
        }

        [HttpGet, Route("summary")]
        public IActionResult GetSummary()
            => CustomResponse(_service.Summary());
    }
}

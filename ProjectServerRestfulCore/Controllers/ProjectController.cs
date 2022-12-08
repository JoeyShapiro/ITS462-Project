using Microsoft.AspNetCore.Mvc;

namespace ProjectServerRestfulCore.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        [Route("GetDevices")]
        [HttpGet]
        public IActionResult GetDevices()
        {
            return Content("");
        }

        [Route("GetDeviceDetails")]
        [HttpGet]
        public IActionResult GetDeviceDetails([FromQuery] int id)
        {
            return Content("");
        }

        [Route("GetFilters")]
        [HttpGet]
        public IActionResult GetFilters()
        {
            return Content("");
        }

        [Route("GetFilteredDevices")]
        [HttpGet]
        public IActionResult GetFiltedDevices([FromQuery] string filter)
        {
            return Content("");
        }
    }
}

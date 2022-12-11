using Microsoft.AspNetCore.Mvc;
using ProjectServerRestful.Services;

namespace ProjectServerRestfulCore.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        [Route("GetDevices")]
        [HttpGet]
        public IActionResult GetDevices()
        {
            return Content(Database.GetDevicesFromDBAsXML("CALL list_devices();"));
        }

        [Route("GetDeviceDetails")]
        [HttpGet]
        public IActionResult GetDeviceDetails([FromQuery] int id)
        {
            return Content(Database.GetDeviceDetailsFromDBAsXML("CALL get_device_details(" + id + ");"));
        }

        [Route("GetFilters")]
        [HttpGet]
        public IActionResult GetFilters()
        {
            return Content(Database.GetDeviceFiltersFromDBAsXML());
        }

        [Route("GetFilteredDevices")]
        [HttpGet]
        public IActionResult GetFiltedDevices([FromQuery] string filter, string chosen)
        {
            return Content(Database.GetDevicesFromDBAsXML("CALL list_filtered_devices(\""+filter+"\", \""+chosen+"\");"));
        }
    }
}

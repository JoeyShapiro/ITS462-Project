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

        [Route("Scrape")]
        [HttpGet]
        public IActionResult Scrape([FromQuery] string super_secret_passphrase)
        {
            // authentication check
            if (super_secret_passphrase != "project")
                return Content("Invalid token");

            // clean database, there could be repeats
            Database.CleanDB();

            // rescrape websites
            var neweggs =  ScraperService.ScrapeAndInsertFromNewEgg();
            var bestbuys = ScraperService.ScrapeAndInsertFromBestBuy();

            // return the amount of affected rows for each scrape
            return Content("Items successfullly added (Max = "+ScraperService.MAX_AFFECTED+"); newegg: "+neweggs+"; bestbuy: "+bestbuys);
        }
    }
}

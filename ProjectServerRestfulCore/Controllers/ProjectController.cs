using Microsoft.AspNetCore.Mvc;
using ProjectServerRestful.Services;

namespace ProjectServerRestfulCore.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        // gets all devices (id, model, price)
        [Route("GetDevices")]
        [HttpGet]
        public IActionResult GetDevices()
        {
            return Content(Database.GetDevicesFromDBAsXML("CALL list_devices();"));
        }

        // get details of a device (select * from devices where id=id)
        [Route("GetDeviceDetails")]
        [HttpGet]
        public IActionResult GetDeviceDetails([FromQuery] int id)
        {
            return Content(Database.GetDeviceDetailsFromDBAsXML("CALL get_device_details(" + id + ");"));
        }

        // gets all filters to use (all cols of a table)
        [Route("GetFilters")]
        [HttpGet]
        public IActionResult GetFilters()
        {
            return Content(Database.GetDeviceFiltersFromDBAsXML());
        }

        // get all devices using a filter (like search)
        [Route("GetFilteredDevices")]
        [HttpGet]
        public IActionResult GetFiltedDevices([FromQuery] string filter, string chosen)
        {
            return Content(Database.GetDevicesFromDBAsXML("CALL list_filtered_devices(\""+filter+"\", \""+chosen+"\");"));
        }

        // scrapes the websites and puts in db. WARNING truncates table
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

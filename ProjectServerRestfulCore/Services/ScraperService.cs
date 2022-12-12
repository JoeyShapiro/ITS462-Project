using HtmlAgilityPack;
using System;

namespace ProjectServerRestful.Services
{
	public class ScraperService //: IHostedService
	{

		// best way, considering sites are different
		public static int ScrapeAndInsertFromNewEgg()
		{
			// this is scraping all the desktop computers
            var url = "https://www.newegg.com/Desktop-Computers/SubCategory/ID-10?Tid=19096";
            var web = new HtmlWeb();
            var doc = web.Load(url);
			var affected = 0;

			var devices = doc.DocumentNode.SelectNodes("//*[@class='item-container']");

			foreach ( var device in devices )
			{
				var id = device.Attributes["id"].Value;

				// link, specs
				var tag_specs = device.SelectNodes(".//a[@class='item-title']")[0];
				var link = tag_specs.Attributes["href"].Value;
				var specs = tag_specs.InnerHtml;

				// brand
				var tag_brand = device.SelectNodes(".//a[@class='item-brand']");
				var brand = tag_brand?.FirstOrDefault()?.SelectNodes(".//img")[0].Attributes["alt"].Value ?? "unknown";

				// price
				var tag_price = device.SelectNodes(".//li[@class='price-current']");
				var price = double.Parse(tag_price[0].SelectNodes(".//strong")[0].InnerHtml + tag_price[0].SelectNodes(".//sup")[0].InnerHtml);

				// go to link (the page of the computer)
				doc = web.Load(link);
				var tag_info = doc.DocumentNode.SelectNodes("//table[@class='table-horizontal']//tr");

                // desc
                var tag_desc = doc.DocumentNode.SelectNodes("//div[@id='arimemodetail']//b");
				var desc = tag_desc?.FirstOrDefault()?.InnerHtml ?? ""; // if it cant find a desc, set it to unknown;
																		// model
				var tag_model = tag_info.Where(s => s.InnerHtml.StartsWith("<th>Model<!-- --> </th>"));
				var model = tag_model?.FirstOrDefault()?.InnerHtml.Split("<th>Model<!-- --> </th>")[1].Trim().Replace("<td>", "").Replace("</td>", "") ?? "unknown";
				// type
				var tag_type = tag_info.Where(s => s.InnerHtml.StartsWith("<th>Form Factor"));
				var type = tag_type?.FirstOrDefault()?.InnerHtml.Split("</th>")[1].Trim().Replace("<td>", "").Replace("</td>", "") ?? "desktop"; // is "Desktop Tower"
				type = "desktop";

				// add to database and increase the counter by 1. if it fails, we dont care
				affected += Database.AddDevice(type, brand, model, price, link, desc, specs);
            }

            return affected;
        }

		public static int ScrapeAndInsertFromBestBuy()
		{
			return 0;
		}

		//public Task StartAsync(CancellationToken cancellationToken)
		//{
		//    TimeSpan interval = TimeSpan.FromHours(24);
		//    //calculate time to run the first time & delay to set the timer
		//    //DateTime.Today gives time of midnight 00.00
		//    var nextRunTime = DateTime.Today.AddDays(1).AddHours(1);
		//    var curTime = DateTime.Now;
		//    var firstInterval = nextRunTime.Subtract(curTime);

		//    Action action = () =>
		//    {
		//        //var t1 = Task.Delay(firstInterval);
		//        //t1.Wait();
		//        ////remove inactive accounts at expected time
		//        //RemoveScheduledAccounts(null);
		//        ////now schedule it to be called every 24 hours for future
		//        //// timer repeates call to RemoveScheduledAccounts every 24 hours.
		//        //_timer = new Timer(
		//        //    RemoveScheduledAccounts,
		//        //    null,
		//        //    TimeSpan.Zero,
		//        //    interval
		//        //);
		//    };

		//    // no need to await this call here because this task is scheduled to run much much later.
		//    Task.Run(action);
		//    return Task.CompletedTask;
		//}

		//public Task StopAsync(CancellationToken cancellationToken)
		//{
		//    return Task.CompletedTask;
		//}
	}
}

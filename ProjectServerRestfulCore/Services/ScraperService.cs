namespace ProjectServerRestful.Services
{
    public class ScraperService //: IHostedService
    {
        public ScraperService(string uri)
        {

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

using System;

namespace LearnWordsFast.API.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
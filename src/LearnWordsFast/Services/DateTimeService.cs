﻿using System;

namespace LearnWordsFast.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
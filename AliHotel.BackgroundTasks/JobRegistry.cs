using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace AliHotel.BackgroundTasks
{
    public class JobRegistry : Registry
    {
        public JobRegistry()
        {
            Schedule<DeleteUnconfirmedEmailsJob>().ToRunOnceIn(5).Seconds();
        }
    }
}

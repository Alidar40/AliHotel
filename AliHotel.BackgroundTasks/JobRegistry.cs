using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace AliHotel.BackgroundTasks
{
    class JobRegistry : Registry
    {
        public JobRegistry()
        {
            Schedule<DeleteUnconfirmedEmailsJob>().ToRunEvery(1).Days();
        }
    }
}

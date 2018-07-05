using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AliHotel.BackgroundTasks;
using FluentScheduler;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ninject;

namespace AliHotel.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            JobManager.JobFactory = new JobFactory(new StandardKernel(new BackgroundTasksNinjectModule()));
            JobManager.Initialize(new JobRegistry());

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

using FluentScheduler;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;

namespace AliHotel.BackgroundTasks
{
    public class JobFactory : IJobFactory
    {
        private IKernel Kernel { get; }

        public JobFactory(IKernel kernel)
        {
            Kernel = kernel;
        }

        public IJob GetJobInstance<T>() where T : IJob
        {
            return Kernel.Get<T>();
        }
    }
}

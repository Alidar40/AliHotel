using FluentScheduler;
using System;
using System.Web;
using AliHotel.Domain.Interfaces;

namespace AliHotel.BackgroundTasks
{
    public class DeleteUnconfirmedEmailsJob : IJob
    {
        private readonly object _lock = new object();
        private readonly IAuthorizationService _authorizationService;

        private bool _shuttingDown;

        public DeleteUnconfirmedEmailsJob(IAuthorizationService authorizationService)
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            //HostingEnvironment.RegisterObject(this);
            _authorizationService = authorizationService;
        }

        public void Execute()
        {
            try
            {
                lock (_lock)
                {
                    if (_shuttingDown)
                        return;

                    _authorizationService.DeleteUsersWithUncofirmedEmails();
                }
            }
            finally
            {
                // Always unregister the job when done.
                //HostingEnvironment.UnregisterObject(this);
            }
        }

        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                _shuttingDown = true;
            }

            //HostingEnvironment.UnregisterObject(this);
        }
    }
}

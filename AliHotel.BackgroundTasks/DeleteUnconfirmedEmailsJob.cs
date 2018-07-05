using FluentScheduler;
using System;
using System.Web;
using AliHotel.Domain.Interfaces;
using AliHotel.Database;
using System.Linq;

namespace AliHotel.BackgroundTasks
{
    public class DeleteUnconfirmedEmailsJob : IJob
    {
        private readonly object _lock = new object();
        //private readonly IAuthorizationService _authorizationService;
        private readonly DatabaseContext _context;

        private bool _shuttingDown;

        //public DeleteUnconfirmedEmailsJob(IAuthorizationService authorizationService)
        public DeleteUnconfirmedEmailsJob(DatabaseContext context)
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            //HostingEnvironment.RegisterObject(this);

            //_authorizationService = authorizationService;

            _context = context;
        }

        public void Execute()
        {
            try
            {
                lock (_lock)
                {
                    if (_shuttingDown)
                        return;

                    //_authorizationService.DeleteUsersWithUncofirmedEmails();

                    var usersToDelete = _context.Users.Where(u => u.EmailConfirmed == false).ToList();
                    _context.Users.RemoveRange(usersToDelete);

                    foreach (var user in usersToDelete)
                    {
                        if ((user.RegistrationTime - DateTime.UtcNow).Seconds > 30)
                        {
                            _context.Remove(user);
                        }
                    }

                    _context.SaveChanges();
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

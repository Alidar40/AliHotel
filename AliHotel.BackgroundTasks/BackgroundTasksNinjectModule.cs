using AliHotel.Database;
using AliHotel.Domain.Interfaces;
using AliHotel.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace AliHotel.BackgroundTasks
{
    public class BackgroundTasksNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<DatabaseContext>();
            //Bind<IAuthorizationService>().To<AuthorizationService>();
        }
    }
}

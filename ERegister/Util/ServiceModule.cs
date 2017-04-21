using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERegister.BLL.Interfaces;
using ERegister.BLL.Services;
using ERegister.DAL.Models;
using ERegister.DAL.Models.Interfaces;
using ERegister.DAL.Models.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;

namespace ERegister.Util
{
    public class ServiceModule:NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAttendService>().To<AttendsService>().InRequestScope();
        }
    }
}
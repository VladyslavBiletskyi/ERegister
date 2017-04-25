using ERegister.BLL.Interfaces;
using ERegister.BLL.Services;
using Ninject.Modules;
using Ninject.Web.Common;

namespace ERegister.Util
{
    public class ServiceModule:NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAttendService>().To<AttendsService>().InRequestScope();
            this.Bind<DAL.Models.Identity.ApplicationUserManager>().ToSelf();
        }
    }
}
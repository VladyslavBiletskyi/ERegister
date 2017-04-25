using ERegister.BLL.Interfaces;
using ERegister.BLL.Services;
using Ninject.Modules;
using Ninject.Web.Common;

namespace ERegister.PL.NinjectModules
{
    public class ServicesModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IAttendsService>().To<AttendsService>().InRequestScope();
        }
    }
}
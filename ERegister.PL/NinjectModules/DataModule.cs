using ERegister.DAL.Models;
using ERegister.DAL.Models.Interfaces;
using ERegister.DAL.Models.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;
using Microsoft.AspNet.Identity;

namespace ERegister.PL.NinjectModules
{
    public class DataModule:NinjectModule
    {
        public override void Load()
        {
            //Bind<ERegisterDbContext>().ToSelf().InRequestScope();
            Bind<IDataContext>().To<ERegisterDbContext>().InRequestScope();
            Bind<IGroupsRepository>().To<GroupsRepository>().InRequestScope();
            Bind<ILessonsRepository>().To<LessonsRepository>().InRequestScope();
            Bind<IAttendsRepository>().To<AttendsRepository>().InRequestScope();
            Bind<IAttendControlsRepository>().To<AttendControlsRepository>().InRequestScope();
            Bind<IMarksRepository>().To<MarksRepository>().InRequestScope();
        }
    }
}
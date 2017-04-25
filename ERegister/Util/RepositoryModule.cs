using ERegister.DAL.Models;
using ERegister.DAL.Models.Interfaces;
using ERegister.DAL.Models.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;

namespace ERegister.Util
{
    public class RepositoryModule:NinjectModule
    {
        public override void Load()
        {
            this.Bind<ERegisterDbContext>().ToSelf().InRequestScope();
            this.Bind<IDataContext>().To<ERegisterDbContext>().InRequestScope();

            //this.Bind<DbContext>().ToMethod(context => (DbContext) context.Kernel.GetService(typeof(IDataContext))).InRequestScope();

            this.Bind<IGroupsRepository>().To<GroupsRepository>().InRequestScope();
            this.Bind<ILessonsRepository>().To<LessonsRepository>().InRequestScope();
            this.Bind<IAttendsRepository>().To<AttendsRepository>().InRequestScope();
            this.Bind<IAttendControlsRepository>().To<AttendControlsRepository>().InRequestScope();
            this.Bind<IMarksRepository>().To<MarksRepository>().InRequestScope();
        }
    }
}
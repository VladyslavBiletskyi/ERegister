using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            this.Bind<IDataContext>().To<ERegisterDbContext>().InRequestScope();
            this.Bind<IRepository<Group>>().To<GroupsRepository>().InRequestScope();
            this.Bind<IRepository<Lesson>>().To<LessonsRepository>().InRequestScope();
            this.Bind<IRepository<Attend>>().To<AttendsRepository>().InRequestScope();
            this.Bind<IRepository<Mark>>().To<MarksRepository>().InRequestScope();
        }
    }
}
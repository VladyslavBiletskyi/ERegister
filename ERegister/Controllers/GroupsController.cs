using System.Collections.Generic;
using System.Web.Http;
using ERegister.DAL.Models;
using ERegister.DAL.Models.Interfaces;
using ERegister.DAL.Models.ViewModels;
using WebGrease.Css.Extensions;

namespace ERegister.Controllers
{
    [RoutePrefix("api/Groups")]
    public class GroupsController : ApiController
    {
        private IGroupsRepository repository;

        public GroupsController(IGroupsRepository repository)
        {
            this.repository = repository;
        }
        [Route("Get")]
        public List<GroupViewModel> GetAllGroups()
        {

            List<GroupViewModel> groups = new List<GroupViewModel>(); 
            repository.GetAll().ForEach(x=>groups.Add(new GroupViewModel{Id=x.Id, Name = x.Name}));
            return groups;
        }
    }
}

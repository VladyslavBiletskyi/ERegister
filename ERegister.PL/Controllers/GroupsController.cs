using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ERegister.DAL.Models.Interfaces;
using ERegister.DAL.Models.ViewModels;

namespace ERegister.PL.Controllers
{
    [RoutePrefix("api/Groups")]
    public class GroupsController : ApiController
    {
        private IGroupsRepository groupsRepository;

        public GroupsController(IGroupsRepository groupsRepository)
        {
            this.groupsRepository = groupsRepository;
        }
        [Route("Get")]
        public List<GroupViewModel> GetGroups()
        {
            List<GroupViewModel> groups = new List<GroupViewModel>();
            groupsRepository.GetAll().ToList().ForEach(x => groups.Add(new GroupViewModel {Name = x.Name, Id = x.Id}));
            return groups;
        }
    }
}

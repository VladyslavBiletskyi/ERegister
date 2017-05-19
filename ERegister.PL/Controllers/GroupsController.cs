using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ERegister.DAL.Models;
using ERegister.DAL.Models.Interfaces;
using ERegister.DAL.Models.ViewModels;
using ERegister.PL.ViewModels;
using WebGrease.Css.Extensions;
using static System.String;

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
        [HttpGet]
        public List<GroupViewModel> GetGroups()
        {
            List<GroupViewModel> groups = new List<GroupViewModel>();
            groupsRepository.GetAll().Where(x=>x.Name!="Teachers").ToList().ForEach(x => groups.Add(new GroupViewModel {Name = x.Name, Id = x.Id}));
            return groups;
        }
        [Route("GetStudents")]
        [HttpGet]
        public List<StudentViewModel> GetStudents(int groupId)
        {
            List<StudentViewModel> students = new List<StudentViewModel>();
            groupsRepository.GetAll().FirstOrDefault(x=>x.Id==groupId)?.Students.ForEach(x => students.Add(new StudentViewModel { UserName = x.FirstName+" "+x.LastName, UserId = x.Id }));
            return students;
        }

        [Route("AddGroup")]
        [HttpPost]
        public async Task<IHttpActionResult> AddGroup(GroupViewModel model)
        {
            if (IsNullOrEmpty(model.Name)||groupsRepository.GetAll().Any(x => x.Name == model.Name))
            {
                return BadRequest("Group already exist");
            }
            Group group = new Group {Name = model.Name};
            groupsRepository.Add(group);
            groupsRepository.SaveChanges();
            return Ok();
        }

    }
}

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ERegister.DAL.Models;
using ERegister.DAL.Models.Interfaces;
using ERegister.DAL.Models.ViewModels;
using static System.String;

namespace ERegister.PL.Controllers
{
    [RoutePrefix("api/Subjects")]
    public class SubjectsController : ApiController
    {
        private ISubjectsRepository subjectsRepository;

        public SubjectsController(ISubjectsRepository subjectsRepository)
        {
            this.subjectsRepository = subjectsRepository;
        }

        [HttpGet]
        [Authorize(Roles ="Teacher")]
        public List<SubjectViewModel> GetSubjects()
        {
            List<SubjectViewModel> subjects = new List<SubjectViewModel>();
            subjectsRepository.GetAll().ToList().ForEach(x => subjects.Add(new SubjectViewModel { Name = x.Name, Id = x.Id }));
            return subjects;
        }
        
        [Route("AddSubject")]
        [HttpPost]
        public async Task<IHttpActionResult> AddSubject(SubjectViewModel model)
        {
            if (IsNullOrEmpty(model.Name) || subjectsRepository.GetAll().Any(x => x.Name == model.Name))
            {
                return BadRequest("Group already exist");
            }
            Subject subject = new Subject { Name = model.Name };
            subjectsRepository.Add(subject);
            subjectsRepository.SaveChanges();
            return Ok();
        }
    }
}

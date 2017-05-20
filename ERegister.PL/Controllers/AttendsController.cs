using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using ERegister.DAL.Models;
using ERegister.DAL.Models.Interfaces;
using ERegister.PL.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using System.Linq;

namespace ERegister.PL.Controllers
{
    [RoutePrefix("api/Attends")]
    public class AttendsController : ApiController
    {
        private IAttendControlsRepository controlsRepository;
        private IAttendsRepository attendsRepository;
        private ILessonsRepository lessonsRepository;
        private ApplicationUserManager _userManager;

        public AttendsController(IAttendControlsRepository controls, IAttendsRepository attends, ILessonsRepository lessonsRepository)
        {
            this.controlsRepository = controls;
            this.attendsRepository = attends;
            this.lessonsRepository = lessonsRepository;
        }
        [Inject]
        public IDataContext Context { private get; set; }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? (_userManager = new ApplicationUserManager(new UserStore<ApplicationUser>((DbContext)Context)));
            }
            private set
            {
                _userManager = value;
            }
        }
        [Route("SetAttend")]
        [HttpPost]
        public async Task<IHttpActionResult> SetAttendOfStudent(AttendControlViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong data");
            }
            ApplicationUser user = await UserManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            AttendControl control = controlsRepository.GetAll().FirstOrDefault(x=>x.Id==model.ControllerId);
            if (control == null)
            {
                return BadRequest("Controller not found");
            }
            if (control.Lesson.BeginigDateTime.Date != DateTime.Now.Date||
                control.Lesson.BeginigDateTime.AddHours(1).AddMinutes(35) < DateTime.Now)
            {
                return BadRequest("Wrong lesson");
            }
            if (await attendsRepository.GetAll().AnyAsync(x => x.AttendControl.Lesson.Id == control.Lesson.Id))
            {
                return BadRequest("User already attended the lesson");
            }
            attendsRepository.Add(new Attend
            {
                Student = user,
                AttendControl = control
            });
            attendsRepository.SaveChanges();
            return Ok(user.FirstName+" "+user.LastName+" attended the lesson");
        }
    }
}

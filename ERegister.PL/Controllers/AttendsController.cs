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
        private const int LessonHoursLength = 1;
        private const int LessonMinutesLength = 35;
        private IAttendsRepository attendsRepository;
        private ILessonsRepository lessonsRepository;
        private ApplicationUserManager _userManager;

        public AttendsController(IAttendsRepository attends, ILessonsRepository lessonsRepository)
        {
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

            Lesson lesson = lessonsRepository.GetAll().Where(x => x.ControllerId==model.ControllerId &&
              x.BeginigDateTime.Date <= DateTime.Now.Date &&
              x.BeginigDateTime.AddHours(LessonHoursLength).AddMinutes(LessonMinutesLength) >= DateTime.Now).ToList().LastOrDefault();
            if (lesson == null)
            {
                return BadRequest("Wrong lesson");
            }
            if (await attendsRepository.GetAll().AnyAsync(x=>x.Lesson.Id==lesson.Id && x.Student.Id==user.Id))
            {
                return BadRequest("User already attended the lesson");
            }
            attendsRepository.Add(new Attend
            {
                Student = user,
                Lesson = lesson
            });
            attendsRepository.SaveChanges();
            return Ok(user.FirstName+" "+user.LastName+" attended the lesson");
        }
    }
}

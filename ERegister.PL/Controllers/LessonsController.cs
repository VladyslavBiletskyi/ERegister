using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using ERegister.BLL.DTOs;
using ERegister.BLL.Interfaces;
using ERegister.DAL.Models;
using ERegister.DAL.Models.Interfaces;
using ERegister.PL.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;

namespace ERegister.PL.Controllers
{
    [RoutePrefix("api/Lessons")]
    [Authorize]
    public class LessonsController : ApiController
    {

        private ILessonsRepository lessonsRepository;
        private IAttendControlsRepository attendControlsRepository;
        private ISubjectsRepository subjectsRepository;
        private IGroupSubjectsRepository groupSubjectsRepository;
        private IGroupsRepository groupsRepository;
        private IDebtsService debtsService;
        private ApplicationUserManager _userManager;

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

        public LessonsController(ILessonsRepository lessons, IAttendControlsRepository attends, IDebtsService attendsService, ISubjectsRepository subjectsRepository, IGroupSubjectsRepository groupSubjectsRepository, IGroupsRepository groupsRepository)
        {
            this.lessonsRepository = lessons;
            this.attendControlsRepository = attends;
            this.debtsService = attendsService;
            this.subjectsRepository = subjectsRepository;
            this.groupSubjectsRepository = groupSubjectsRepository;
            this.groupsRepository = groupsRepository;
        }

        [Route("AddLesson")]
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IHttpActionResult> AddLesson(AddLessonViewModel model)
        {
            if (model.BeginingDateTime.Date < DateTime.Now.Date)
            {
                return BadRequest("Wrong date or time");
            }
            Subject subject = await subjectsRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.SubjectId);
            if (subject == null)
            {
                return BadRequest("Wrong subject");
            }
            SubjectOfTheGroup groupSubject = await groupSubjectsRepository.GetAll().FirstOrDefaultAsync(x => x.Subject == subject);
            if (groupSubject == null)
            {
                groupSubject = new SubjectOfTheGroup
                {
                    Group = await groupsRepository.GetAll().FirstOrDefaultAsync(y => y.Id == model.GroupId),
                    Subject = subject,
                    Teacher = await UserManager.FindByIdAsync(User.Identity.GetUserId())
                };
                groupSubjectsRepository.Add(groupSubject);
                groupSubjectsRepository.SaveChanges();
            }
            lessonsRepository.Add(new Lesson
            {
                BeginigDateTime = model.BeginingDateTime,
                Room = model.Room,
                Subject = groupSubject,
                Teacher = await UserManager.FindByIdAsync(User.Identity.GetUserId())
            });
            lessonsRepository.SaveChanges();
            return Ok();
        }

        [Route("Absents")]
        [HttpGet]
        public List<LessonViewModel> GetAbsents()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            if (user?.Group == null)
            {
                return null;
            }
            var absents = MapToModel(debtsService.GetAbsents(user), user);
            return absents;
        }

        [Route("Debts")]
        [HttpGet]
        public List<LessonViewModel> GetDebts()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            if (user?.Group == null)
            {
                return null;
            }
            var debts = MapToModel(debtsService.GetDebts(user), user);
            return debts;
        }

        [Route("Register")]
        [HttpGet]
        public List<LessonViewModel> GetRegister()
        {
           ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            if (user?.Group == null)
            {
                return null;
            }
            var register = MapToModel(debtsService.GetRegister(user), user);
            return register;
        }

        private List<LessonViewModel> MapToModel(List<LessonDto> dto, ApplicationUser user)
        {
            var model = new List<LessonViewModel>();
            foreach (var element in dto)
            {
                model.Add(new LessonViewModel
                {
                    BeginigDateTime = element.Lesson.BeginigDateTime,
                    AverageMark = element.AverageMark,
                    IsPresent = attendControlsRepository.GetAll()
                                    .FirstOrDefault(x => x.Lesson == element.Lesson)
                                    ?.Attends.Count(x => x.Student == user) != 0,
                    NumberOfPresent = element.NumberOfPresent,
                    Result = element.MyMark,
                    Subject = element.Lesson.Subject.Subject.Name,
                    Teacher = element.Lesson.Teacher.FirstName + element.Lesson.Teacher.LastName
                });
            }
            return model;
        }
    }
}

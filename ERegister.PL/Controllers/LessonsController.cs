using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ERegister.BLL.DTOs;
using ERegister.BLL.Interfaces;
using ERegister.DAL.Models;
using ERegister.DAL.Models.Interfaces;
using ERegister.DAL.Models.ViewModels;
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

        public LessonsController(ILessonsRepository lessons,IDebtsService attendsService, ISubjectsRepository subjectsRepository, IGroupSubjectsRepository groupSubjectsRepository, IGroupsRepository groupsRepository)
        {
            this.lessonsRepository = lessons;
            this.debtsService = attendsService;
            this.subjectsRepository = subjectsRepository;
            this.groupSubjectsRepository = groupSubjectsRepository;
            this.groupsRepository = groupsRepository;
        }

        [Route("Get")]
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<List<LessonRegisterViewModel>> GetLessons(int groupSubjectId)
        {
            var lessons = new List<LessonRegisterViewModel>();
            await lessonsRepository.GetAll()
                .Where(x => x.Subject.Id == groupSubjectId)
                .ForEachAsync(x => lessons.Add(new LessonRegisterViewModel
                {
                    BeginingDateTime = x.BeginigDateTime,
                    LessonId = x.Id
                }));
            return lessons;
        }

        [Route("GetLastLesson")]
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public int GetLastLesson(int groupId)
        {
            Lesson lesson = lessonsRepository.GetAll().Where(x=>x.Subject.Group.Id==groupId).ToList().LastOrDefault();
            return lesson?.Id ?? -1;
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
            if (model.ControllerId == 0)
            {
                return BadRequest("Wrong Controller Id");
            }
            Subject subject = await subjectsRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.SubjectId);
            if (subject == null)
            {
                return BadRequest("Wrong subject");
            }
            SubjectOfTheGroup groupSubject = await groupSubjectsRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Subject.Id == subject.Id);
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
            Lesson lesson = new Lesson
            {
                BeginigDateTime = model.BeginingDateTime,
                Room = model.Room,
                Subject = groupSubject,
                Teacher = await UserManager.FindByIdAsync(User.Identity.GetUserId()),
                ControllerId=model.ControllerId
            };
            lessonsRepository.Add(lesson);
            try
            {
                lessonsRepository.SaveChanges();
            }
            catch (Exception e)
            {

            }
            return Ok();
        }

        [Route("AddMark")]
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IHttpActionResult AddMark(AddMarksViewModel model)
        {
            Lesson lesson = lessonsRepository.GetAll()
                .FirstOrDefault(x => x.Id == model.LessonId);
            if (lesson == null)
            {
                return BadRequest("Wrong Lesson");
            }
            if (model.Marks == null)
            {
                return BadRequest("Troubles with marks");
            }
            foreach (var element in model.Marks)
            {
                if (element.Score == 0)
                {
                    continue;
                }
                ApplicationUser user;
                if ((user = UserManager.FindById(element.UserId)) != null)
                {
                    if (lesson.Marks.All(x => x.Student.Id != user.Id) && 
                        lesson.Attends.Any(x=>x.Student.Id==user.Id))
                    {
                        lesson.Marks.Add(new Mark
                        {
                            Student = user,
                            Teacher = UserManager.FindById(User.Identity.GetUserId()),
                            Result = element.Score
                        });
                    }
                }
            }
            lessonsRepository.SaveChanges();
            return Ok();
        }

        [Route("GetGroupsForSubject")]
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public List<GroupViewModel> GetGroupsForSubject(int subject)
        {
            List<GroupViewModel> groups = new List<GroupViewModel>();
            groupSubjectsRepository.GetAll().Where(x => x.Subject.Id == subject).Select(x => x.Group).ToList().ForEach(x => groups.Add(new GroupViewModel { Name = x.Name, Id = x.Id }));
            return groups;
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
                    IsPresent = lessonsRepository.GetAll()
                                    .FirstOrDefault(x => x.Id == element.Lesson.Id)
                                    ?.Attends.Any(x => x.Student.Id == user.Id)??false,
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

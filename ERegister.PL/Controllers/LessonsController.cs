using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        private IAttendControlsRepository attendsRepository;
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

        public LessonsController(ILessonsRepository lessons, IAttendControlsRepository attends, IDebtsService attendsService)
        {
            this.lessonsRepository = lessons;
            this.attendsRepository = attends;
            this.debtsService = attendsService;
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
                    IsPresent = attendsRepository.GetAll()
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

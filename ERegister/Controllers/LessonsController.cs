using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ERegister.BLL.DTOs;
using ERegister.BLL.Interfaces;
using ERegister.DAL.Models;
using ERegister.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ERegister.Controllers
{
    [Authorize]
    [RoutePrefix("api/Lessons")]
    public class LessonsController : ApiController
    {
        private IAttendService attendService;
        public LessonsController(IAttendService attendService)
        {
            this.attendService = attendService;
        }

        private ApplicationUserManager UserManager => HttpContext.Current.GetOwinContext()
            .GetUserManager<ApplicationUserManager>();
        [HttpGet]
        [Route("Absents")]
        public async Task<List<LessonViewModel>> GetAbsents()
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new List<LessonViewModel>();
            }
            List<LessonDto> absents = attendService.GetAbsents(user);
            List<LessonViewModel> model = new List<LessonViewModel>();
            foreach (var element in absents)
            {
                model.Add(new LessonViewModel
                {
                    BeginigDateTime = element.Lesson.BeginigDateTime,
                    IsPresent = false,
                    Subject = element.Lesson.Subject.Subject.Name,
                    Result = 0,
                    Teacher = element.Lesson.Teacher.FirstName + element.Lesson.Teacher.LastName,
                    AverageMark = element.AverageMark,
                    NumberOfPresent = element.NumberOfPresent
                });
            }
            return model;
        }
    }
}
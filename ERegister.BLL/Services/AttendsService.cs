using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERegister.BLL.DTOs;
using ERegister.BLL.Interfaces;
using ERegister.DAL.Models;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.BLL.Services
{
    public class AttendsService:IAttendService
    {
        private IRepository<AttendControl> attendControlsRepository;
        private IRepository<Lesson> lessonsRepository;
        private IRepository<Mark> marksRepository;

        public AttendsService(IRepository<AttendControl> attendControlsRepository, IRepository<Lesson> lessonsRepository, IRepository<Mark> marksRepository)
        {
            this.attendControlsRepository = attendControlsRepository;
            this.lessonsRepository = lessonsRepository;
            this.marksRepository = marksRepository;
        }

        public List<LessonDto> GetAbsents(ApplicationUser user)
        {
            Group group = user.Group;
            List<Lesson> absents = attendControlsRepository.GetAll()
                .Where(x => x.Lesson.Subject.Group == group && x.Attends.All(y=>y.Student !=user))
                .Select(x => x.Lesson)
                .Distinct().ToList();
            List<LessonDto> answer = new List<LessonDto>();
            foreach (var element in absents)
            {
                answer.Add(new LessonDto
                {
                    Lesson = element,
                    NumberOfPresent = attendControlsRepository.GetAll()
                                          .FirstOrDefault(x => x.Lesson == element)
                                          ?.Attends.Count ?? 0,
                    AverageMark = marksRepository.GetAll().Where(x => x.Lesson == element).Average(x => x.Result)
                });
            }
            return answer;
        }
    }
}

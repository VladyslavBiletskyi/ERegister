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
    public class DebtsService:IDebtsService
    {
        private IAttendControlsRepository attendControlsRepository;
        private ILessonsRepository lessonsRepository;
        private IMarksRepository marksRepository;

        public DebtsService(IAttendControlsRepository attendControlsRepository, ILessonsRepository lessonsRepository, IMarksRepository marksRepository)
        {
            this.attendControlsRepository = attendControlsRepository;
            this.lessonsRepository = lessonsRepository;
            this.marksRepository = marksRepository;
        }

        public List<LessonDto> GetAbsents(ApplicationUser user)
        {
            Group group = user.Group;
            if (group == null)
            {
                return null;
            }
            List<Lesson> absents = attendControlsRepository.GetAll()
                .Where(x => x.Lesson.Subject.Group.Id == group.Id && x.Attends.All(y=>y.Student.Id != user.Id))
                .Select(x => x.Lesson)
                .Distinct().ToList();
            List<LessonDto> answer = new List<LessonDto>();
            foreach (var element in absents)
            {
                answer.Add(new LessonDto
                {
                    Lesson = element,
                    NumberOfPresent = attendControlsRepository.GetAll()
                                          .FirstOrDefault(x => x.Lesson.Id == element.Id)
                                          ?.Attends.Count ?? 0,
                    AverageMark = marksRepository.GetAll().
                    Where(x => x.Lesson.Id == element.Id).Average(x => x.Result)
                });
            }
            return answer;
        }

        public List<LessonDto> GetDebts(ApplicationUser user)
        {
            Group group = user.Group;
            if (group == null)
            {
                return null;
            }
            List<Lesson> debts = lessonsRepository.GetAll()
                .Where(x => x.Subject.Group.Id == group.Id && x.Marks.Count > 0 && x.Marks.Count(y => y.Student.Id == user.Id && y.Result<60) == 0)
                .Select(x => x)
                .Distinct()
                .ToList();
            List<LessonDto> answer = new List<LessonDto>();
            foreach (var element in debts)
            {
                answer.Add(new LessonDto
                {
                    Lesson = element,
                    NumberOfPresent = attendControlsRepository.GetAll()
                                          .FirstOrDefault(x => x.Lesson.Id == element.Id)
                                          ?.Attends.Count ?? 0,
                    AverageMark = marksRepository.GetAll().
                        Where(x => x.Lesson.Id == element.Id).Average(x => x.Result),
                    MyMark = element.Marks.FirstOrDefault(x=>x.Student.Id == user.Id)?.Result??0
                });
            }
            return answer;
        }
        public List<LessonDto> GetRegister(ApplicationUser user)
        {
            Group group = user.Group;
            if (group == null)
            {
                return null;
            }
            List<Lesson> debts = lessonsRepository.GetAll()
                .Where(x => x.Subject.Group.Id == group.Id)
                .Select(x => x)
                .Distinct()
                .ToList();
            List<LessonDto> answer = new List<LessonDto>();
            foreach (var element in debts)
            {
                answer.Add(new LessonDto
                {
                    Lesson = element,
                    NumberOfPresent = attendControlsRepository.GetAll()
                                          .FirstOrDefault(x => x.Lesson.Id == element.Id)
                                          ?.Attends.Count ?? 0,
                    AverageMark = marksRepository.GetAll().
                        Where(x => x.Lesson.Id == element.Id).Average(x => x.Result),
                    MyMark = element.Marks.FirstOrDefault(x => x.Student.Id == user.Id)?.Result ?? 0
                });
            }
            return answer;
        }
    }
}

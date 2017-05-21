using System;
using System.Collections.Generic;
using System.Linq;
using ERegister.BLL.DTOs;
using ERegister.BLL.Interfaces;
using ERegister.DAL.Models;
using ERegister.DAL.Models.Interfaces;

namespace ERegister.BLL.Services
{
    public class DebtsService:IDebtsService
    {
        private ILessonsRepository lessonsRepository;
        private IMarksRepository marksRepository;

        public DebtsService(ILessonsRepository lessonsRepository, IMarksRepository marksRepository)
        {
            this.lessonsRepository = lessonsRepository;
            this.marksRepository = marksRepository;
        }

        public List<LessonDto> GetAbsents(ApplicationUser user)
        {
            var nowDate = DateTime.Now;
            Group group = user.Group;
            if (group == null)
            {
                return null;
            }
            var lessons = lessonsRepository.GetAll()
                .Where(x => x.Subject.Group.Id == group.Id
                            && x.BeginigDateTime < nowDate
                            && x.Attends.All(y => y.Student.Id != user.Id))
                .ToList();
            List<LessonDto> answer = new List<LessonDto>();
            foreach (var element in lessons)
            {
                answer.Add(new LessonDto
                {
                    Lesson = element,
                    NumberOfPresent = lessonsRepository.GetAll()
                                          .FirstOrDefault(x => x.Id == element.Id)
                                          ?.Attends.Count ?? 0,
                    AverageMark = marksRepository.GetAll().Any()
                        ? marksRepository.GetAll().Where(x => x.Lesson.Id == element.Id).Average(x => x.Result)
                        : 0
                });
            }
            return answer;
        }

        public List<LessonDto> GetDebts(ApplicationUser user)
        {
            var nowDate = DateTime.Now;
            Group group = user.Group;
            if (group == null)
            {
                return null;
            }
            List<Lesson> debts = lessonsRepository.GetAll()
                .Where(x => x.Subject.Group.Id == group.Id
                            && x.BeginigDateTime < nowDate
                            && x.Marks.Count > 0
                            && x.Marks.Count(y => y.Student.Id == user.Id
                                                  && y.Result < 60) == 0)
                .Select(x => x)
                .Distinct()
                .ToList();
            List<LessonDto> answer = new List<LessonDto>();
            foreach (var element in debts)
            {
                answer.Add(new LessonDto
                {
                    Lesson = element,
                    NumberOfPresent = lessonsRepository.GetAll()
                                          .FirstOrDefault(x => x.Id == element.Id)
                                          ?.Attends.Count ?? 0,
                    AverageMark = marksRepository.GetAll().Any()
                        ? marksRepository.GetAll().Where(x => x.Lesson.Id == element.Id).Average(x => x.Result)
                        : 0,
                    MyMark = element.Marks.FirstOrDefault(x => x.Student.Id == user.Id)?.Result ?? 0
                });
            }
            return answer;
        }

        public List<LessonDto> GetRegister(ApplicationUser user)
        {
            var nowDate = DateTime.Now;
            Group group = user.Group;
            if (group == null)
            {
                return null;
            }
            List<Lesson> debts = lessonsRepository.GetAll()
                .Where(x => x.Subject.Group.Id == group.Id
                            && x.BeginigDateTime < nowDate)
                .Select(x => x)
                .Distinct()
                .ToList();
            List<LessonDto> answer = new List<LessonDto>();
            foreach (var element in debts)
            {
                answer.Add(new LessonDto
                {
                    Lesson = element,
                    NumberOfPresent = lessonsRepository.GetAll()
                                          .FirstOrDefault(x => x.Id == element.Id)
                                          ?.Attends.Count ?? 0,
                    AverageMark = marksRepository.GetAll().Any()
                        ? marksRepository.GetAll().Where(x => x.Lesson.Id == element.Id).Average(x => x.Result)
                        : 0,
                    MyMark = element.Marks.FirstOrDefault(x => x.Student.Id == user.Id)?.Result ?? 0
                });
            }
            return answer;
        }
    }
}

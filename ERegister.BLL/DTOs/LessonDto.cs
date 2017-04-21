using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERegister.DAL.Models;

namespace ERegister.BLL.DTOs
{
    public class LessonDto
    {
        public Lesson Lesson { get; set; }
        public int NumberOfPresent { get; set; }
        public double AverageMark { get; set; }
        public int MyMark { get; set; }
    }
}

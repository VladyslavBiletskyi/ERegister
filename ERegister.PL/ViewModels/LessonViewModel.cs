using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERegister.PL.ViewModels
{
    public class LessonViewModel
    {

        public string Subject { get; set; }
        public DateTime BeginigDateTime { get; set; }
        public string Teacher { get; set; }
        public bool IsPresent { get; set; }
        public int Result { get; set; }
        public int NumberOfPresent { get; set; }
        public double AverageMark { get; set; }
    }
}
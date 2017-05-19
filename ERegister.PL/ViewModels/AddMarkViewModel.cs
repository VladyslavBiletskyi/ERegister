using System.Collections.Generic;

namespace ERegister.PL.ViewModels
{
    public class AddMarksViewModel
    {
        public int LessonId { get; set; }
        public List<MarkViewModel> Marks { get; set; }
    }
}
using System.Collections.Generic;
using ERegister.BLL.DTOs;
using ERegister.DAL.Models;

namespace ERegister.BLL.Interfaces
{
    public interface IDebtsService
    {
        List<LessonDto> GetAbsents(ApplicationUser user);
        List<LessonDto> GetDebts(ApplicationUser user);
        List<LessonDto> GetRegister(ApplicationUser user);

    }
}

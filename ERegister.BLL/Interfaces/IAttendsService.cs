using System.Collections.Generic;
using ERegister.BLL.DTOs;
using ERegister.DAL.Models;

namespace ERegister.BLL.Interfaces
{
    public interface IAttendsService
    {
        List<LessonDto> GetAbsents(ApplicationUser user);
    }
}

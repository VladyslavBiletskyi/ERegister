using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ERegister.DAL.Models.Interfaces;
using ERegister.PL.ViewModels;

namespace ERegister.PL.Controllers
{
    [RoutePrefix("api/Attends")]
    public class AttendsController : ApiController
    {
        private IAttendControlsRepository controls;
        private IAttendsRepository attends;

        public AttendsController(IAttendControlsRepository controls, IAttendsRepository attends)
        {
            this.controls = controls;
            this.attends = attends;
        }

        [Route("SetAttend")]
        [HttpPost]
        public bool SetAttendOfStudent(AttendControlViewModel model)
        {
            return true;
        }
    }
}

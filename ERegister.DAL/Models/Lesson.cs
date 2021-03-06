﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERegister.DAL.Models
{
    public class Lesson : BaseEntity
    {
        public virtual ICollection<Mark> Marks { get; set; }
        public virtual SubjectOfTheGroup Subject { get; set; }
        public virtual ApplicationUser Teacher { get; set; }
        public int ControllerId { get; set; }
        public virtual ICollection<Attend> Attends { get; set; }
        public DateTime BeginigDateTime { get; set; }
        public string Room { get; set; }
    }
}
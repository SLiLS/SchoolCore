using System;
using System.Collections.Generic;
using System.Text;

namespace School.DAL.Entities
{
  public  class ClassTeacher
    {
        public int ClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}

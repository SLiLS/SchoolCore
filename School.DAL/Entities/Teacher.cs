using System;
using System.Collections.Generic;
using System.Text;

namespace School.DAL.Entities
{
   public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }
        public string Position { get; set; }
        public List<ClassTeacher> ClassTeachers { get; set; }
    }
}

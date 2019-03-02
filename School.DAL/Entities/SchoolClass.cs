using System;
using System.Collections.Generic;
using System.Text;

namespace School.DAL.Entities
{
   public class SchoolClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public List<ClassTeacher> ClassTeachers { get; set; }   
    }
}

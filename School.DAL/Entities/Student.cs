using System;
using System.Collections.Generic;
using System.Text;

namespace School.DAL.Entities
{
   public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { set; get; }
        public string SurName { get; set; }
        public string Sex { get; set; }
        public int ClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }
    }
}

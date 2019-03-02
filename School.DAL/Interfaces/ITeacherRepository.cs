using System;
using System.Collections.Generic;
using System.Text;
using School.DAL.Entities;


namespace School.DAL.Interfaces
{
   public interface ITeacherRepository : IRepository<Teacher>
    {
        void AddNewClass(ClassTeacher item);
    }
}

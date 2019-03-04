using System;
using System.Collections.Generic;
using System.Text;
using School.BLL.Intefaces;
using School.DAL.Repositories;
using School.DAL.Interfaces;
using System.Linq;

namespace School.BLL.Services
{
 public   class ServiceCreator : IServiceCreator
    {
        public ITeacherService teacherService()
        {
            return new TeacherService(new UnitOfWork());
        }
        public IStudentService studentService()
        {
            return new StudentService(new UnitOfWork());
        }
        public ISchoolClassService schoolClassService()
        {
            return new SchoolClassService(new UnitOfWork());
        }
    }
}

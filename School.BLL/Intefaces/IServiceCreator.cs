using System;
using System.Collections.Generic;
using System.Text;

namespace School.BLL.Intefaces
{
   public interface IServiceCreator
    {
      
        ISchoolClassService schoolClassService();
        IStudentService studentService();
        ITeacherService teacherService();

    }
}

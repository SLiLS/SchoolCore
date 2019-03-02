using System;
using System.Collections.Generic;
using System.Text;

namespace School.DAL.Interfaces
{
   public interface IUnitOfWork :IDisposable
    {
        ITeacherRepository Teachers { get; }
        IStudentRepository Students { get; }
        //IClassRepository Classes { get; }
        void Save();
    }
}

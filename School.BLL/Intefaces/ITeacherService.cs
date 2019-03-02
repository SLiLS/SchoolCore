using System;
using System.Collections.Generic;
using System.Text;
using School.BLL.DTO;

namespace School.BLL.Intefaces
{
   public interface ITeacherService
    {
        void Create(TeacherDTO item);
        void Update(TeacherDTO item);
        void Delete(int id);
        void AddNewClass(ClassTeacherDTO item);
        TeacherDTO Get(int id);
        IEnumerable<TeacherDTO> GetAll();
        void Dispose();
    }
}

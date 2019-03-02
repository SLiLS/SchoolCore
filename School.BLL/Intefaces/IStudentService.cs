using System;
using System.Collections.Generic;
using System.Text;
using School.BLL.DTO;

namespace School.BLL.Intefaces
{
   public interface IStudentService
    {
        void Delete(int id);
        void Create(StudentDTO item);
        void Update(StudentDTO item);
        StudentDTO Get(int id);
        IEnumerable<StudentDTO> GetAll();
        void Dispose();
    }
}

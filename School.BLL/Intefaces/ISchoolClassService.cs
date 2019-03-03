using System;
using System.Collections.Generic;
using System.Text;
using School.BLL.DTO;

namespace School.BLL.Intefaces
{
   public interface ISchoolClassService
    {
        IEnumerable<SchoolClassDTO> GetAll();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using School.DAL.Entities;

namespace School.DAL.Interfaces
{
   public interface IClassRepository 
    {

        IEnumerable<SchoolClass> GetAll();
        int StudentCount(int id);
    }
}

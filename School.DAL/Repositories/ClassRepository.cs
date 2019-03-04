using System;
using System.Text;
using School.DAL.Interfaces;
using School.DAL.EF;
using School.DAL.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace School.DAL.Repositories
{
    public  class ClassRepository : IClassRepository
    {
        SchoolContext db;
        public ClassRepository(SchoolContext context)
        {
            db = context;
        }
        public IEnumerable<SchoolClass> GetAll()
        {
            var s = db.Students.GroupBy(cf => cf.SchoolClassId).Count();
            return db.SchoolClasses;
        }

        public int StudentCount(int id)
        {
            int count=0;
            count += db.Students.Where(s=>s.SchoolClassId==id).Count();

            return count;
        }
    }
}

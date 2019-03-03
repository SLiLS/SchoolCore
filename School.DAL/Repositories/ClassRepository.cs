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
            return db.SchoolClasses;
        }
    }
}

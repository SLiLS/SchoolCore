using System;
using System.Collections.Generic;
using System.Text;
using School.DAL.Interfaces;
using School.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using School.DAL.EF;
using System.Linq;

namespace School.DAL.Repositories
{
  public  class TeacherRepository : ITeacherRepository
    {
        SchoolContext db;
        public TeacherRepository(SchoolContext context)
        {
            db = context;
        }
        public void Create(Teacher item)
        {
            db.Teachers.Add(item);
        }

        public Teacher Get(int id)
        {
            return db.Teachers.Include(s => s.ClassTeachers).ThenInclude(c => c.SchoolClass).Where(i => i.Id == id).FirstOrDefault();
        }
        public IEnumerable<Teacher> GetAll()
        {
            return db.Teachers.Include(s => s.ClassTeachers);
        }
        public void Update(Teacher item)
        {
            db.Entry(item).State = EntityState.Modified;

        }
        public void Delete(int id)
        {
            db.Teachers.Remove(db.Teachers.Find(id));
        }
        public void AddNewClass(ClassTeacher item)
        {
            db.Teachers.Find(item.TeacherId).ClassTeachers.Add(new ClassTeacher { SchoolClassId = item.SchoolClassId, TeacherId = item.TeacherId });

        }
    }
}

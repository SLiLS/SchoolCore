using System;
using System.Collections.Generic;
using System.Text;
using School.DAL.Interfaces;
using School.DAL.EF;
using School.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace School.DAL.Repositories
{
   public class StudentRepository : IStudentRepository
    {
        SchoolContext db;
        public StudentRepository(SchoolContext context)
        {
            db = context;
        }

        public Student Get(int id)
        {

            return db.Students.Include(c => c.SchoolClass).Where(d => d.Id == id).FirstOrDefault();
        }
        public IEnumerable<Student> GetAll()
        {
            return db.Students.Include(c => c.SchoolClass);
        }
        public void Delete(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
        }
        public void Create(Student item)
        {
            db.Students.Add(item);
        }
        public void Update(Student item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }
}

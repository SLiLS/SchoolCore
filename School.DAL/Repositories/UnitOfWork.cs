using System;
using System.Collections.Generic;
using System.Text;
using School.DAL.Interfaces;
using School.DAL.EF;
using School.DAL.Entities;

namespace School.DAL.Repositories
{
   public class UnitOfWork : IUnitOfWork 
    {
        private SchoolContext db;
        private TeacherRepository teacherRepository;
        private StudentRepository studentRepository;




        public UnitOfWork()
        {
            db = new SchoolContext();
        }

        public IStudentRepository Students
        {
            get
            {
                if (studentRepository == null)
                    studentRepository = new StudentRepository(db);
                return studentRepository;
            }
        }
        public ITeacherRepository Teachers
        {
            get
            {
                if (teacherRepository == null)
                    teacherRepository = new TeacherRepository(db);
                return teacherRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

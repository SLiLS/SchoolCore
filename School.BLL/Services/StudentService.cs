using System;
using System.Collections.Generic;
using System.Text;
using School.BLL.Intefaces;
using School.BLL.DTO;
using School.DAL.Repositories;
using School.DAL.Entities;
using AutoMapper;

namespace School.BLL.Services
{
   public class StudentService : IStudentService
    {
        UnitOfWork uow;
        public StudentService()
        {
            if (uow == null)
                uow = new UnitOfWork();
        }
        public void Create(StudentDTO item)
        {

            uow.Students.Create(new Student
            {
                ClassId = item.ClassId,
                MiddleName = item.MiddleName,
                Name = item.Name,
                Sex = item.Sex,
                SurName = item.SurName
            });
            uow.Save();
        }
        public void Delete(int id)
        {
            Student student = uow.Students.Get(id);
            if (student != null)
                uow.Students.Delete(id);
            uow.Save();
        }
        public void Update(StudentDTO item)
        {
            uow.Students.Update(new Student
            {
                ClassId = item.ClassId,
                MiddleName = item.MiddleName,
                Name = item.Name,
                Sex = item.Sex,
                SurName = item.SurName,
                Id = item.Id

            });
            uow.Save();
        }
        public StudentDTO Get(int id)
        {
            Student student = uow.Students.Get(id);
            return new StudentDTO
            {
                ClassId = student.ClassId,
                SurName = student.SurName,
                Id = student.Id,
                MiddleName = student.MiddleName,
                Name = student.Name,
                Sex = student.Sex
            };

        }
        public IEnumerable<StudentDTO> GetAll()
        {
            var map = new MapperConfiguration(c => c.CreateMap<Student, StudentDTO>()).CreateMapper();
            return map.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(uow.Students.GetAll());
        }
        public void Dispose()
        {
            uow.Dispose();

        }
    }
}

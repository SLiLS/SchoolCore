using System;
using System.Collections.Generic;
using System.Text;
using School.BLL.Intefaces;
using School.BLL.DTO;
using School.DAL.Repositories;
using School.DAL.Entities;
using AutoMapper;
using System.Linq;
using School.DAL.Interfaces;


namespace School.BLL.Services
{
   public class StudentService : IStudentService
    {
        IUnitOfWork uow { get; set; }
        public StudentService(IUnitOfWork unitOfWork)
        {

            uow = unitOfWork;
        }
        public void Create(StudentDTO item)
        {

            uow.Students.Create(new Student
            {
                SchoolClassId = item.SchoolClassId,
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
                SchoolClassId = item.SchoolClassId,
                MiddleName = item.MiddleName,
                Name = item.Name,
                Sex = item.Sex,
                SurName = item.SurName,
                Id = item.Id

            });
            uow.Save();
        }
        public IEnumerable<StudentDTO> Search(string sex, string schoolclass)
        {
            IEnumerable<Student> students = uow.Students.GetAll();
            
            if (schoolclass != null && schoolclass != "")
            {
                students = students.Where(s => s.SchoolClass.Name.Contains(schoolclass));
            }
            if (sex != null  && sex!="Все")
            {
                students = students.Where(s => s.Sex.Contains(sex));
            }
            var map = new MapperConfiguration(c => c.CreateMap<Student, StudentDTO>().ForMember(s => s.ClassName, sx => sx.MapFrom(w => w.SchoolClass.Name)).ForMember(s => s.SchoolClassId, sx => sx.MapFrom(w => w.SchoolClass.Id))).CreateMapper();
            return map.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(uow.Students.GetAll());

        }
        public StudentDTO Get(int id)
        {
            Student student = uow.Students.Get(id);
            return new StudentDTO
            {
                SchoolClassId = student.SchoolClassId,
                SurName = student.SurName,
                Id = student.Id,
                MiddleName = student.MiddleName,
                Name = student.Name,
                Sex = student.Sex
            };

        }
        public IEnumerable<StudentDTO> GetAll()
        {
            var map = new MapperConfiguration(c => c.CreateMap<Student, StudentDTO>().ForMember(dto=>dto.ClassName,s=>s.MapFrom(a=>a.SchoolClass.Name))).CreateMapper();
            return map.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(uow.Students.GetAll());
        }
        public void Dispose()
        {
            uow.Dispose();

        }
        
    }
}

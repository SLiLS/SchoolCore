using System;
using System.Collections.Generic;
using System.Text;
using School.BLL.Intefaces;
using School.BLL.DTO;
using School.DAL.Entities;
using School.DAL.Interfaces;
using School.DAL.Repositories;
using System.Linq;
using AutoMapper;

namespace School.BLL.Services
{
  public  class TeacherService : ITeacherService
    {
        IUnitOfWork uow { get; set; }
        public TeacherService(IUnitOfWork unitOfWork)
        {

            uow = unitOfWork;
        }
        public void Create(TeacherDTO item)
        {
            
            var map = new MapperConfiguration(c => c.CreateMap<TeacherDTO, Teacher>()).CreateMapper();
            Teacher teacher = map.Map<TeacherDTO, Teacher>(item);
            uow.Teachers.Create(teacher);
            uow.Save();


        }
        public void Delete(int id)
        {
            Teacher teacher = uow.Teachers.Get(id);
            if (teacher != null)
                uow.Teachers.Delete(id);
            uow.Save();
        }
        public void Update(TeacherDTO item)
        {
            var map = new MapperConfiguration(c => c.CreateMap<TeacherDTO, Teacher>()).CreateMapper();
            Teacher teacher = map.Map<TeacherDTO, Teacher>(item);
            uow.Teachers.Update(teacher);
            uow.Save();
        }
        public TeacherDTO Get(int id)
        {
            var map = new MapperConfiguration(c => c.CreateMap<Teacher, TeacherDTO>()).CreateMapper();
            return map.Map<Teacher, TeacherDTO>(uow.Teachers.Get(id));

        }
        public IEnumerable<TeacherDTO> GetAll()
        {
            //var map = new MapperConfiguration(c => c.CreateMap<Teacher, TeacherDTO>()).CreateMapper();
            //return map.Map<IEnumerable<Teacher>, IEnumerable<TeacherDTO>>(uow.Teachers.GetAll());
            List<TeacherDTO> ListDTO = new List<TeacherDTO>();
            List<Teacher> list = uow.Teachers.GetAll().ToList();
            foreach (Teacher item in list)
            {
                TeacherDTO teacherDTO = new TeacherDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    StudentCount = uow.Teachers.GetStudents(item.Id),
                    MiddleName = item.MiddleName,
                    SurName = item.SurName,
                    Position = item.Position,                    
                };
                ListDTO.Add(teacherDTO);
            }
            return ListDTO;
        }
        public void AddNewClass(ClassTeacherDTO item)
        {
            uow.Teachers.AddNewClass(new ClassTeacher { SchoolClassId = item.ClassId, TeacherId = item.TeacherId });
        }
        public void Dispose()
        {
            uow.Dispose();

        }
        public IEnumerable<SchoolClassDTO> GetTeacherClasses(int id)
        {
            var s = uow.SchoolClasses.GetAll().Where(w => w.Id == id);
            var map = new MapperConfiguration(c => c.CreateMap<SchoolClass, SchoolClassDTO>()).CreateMapper();
            return map.Map<IEnumerable<SchoolClass>, IEnumerable<SchoolClassDTO>>(s);
        }

        public IEnumerable<TeacherDTO> GetTop3()
        {
            var teachersDTO = GetAll();
            return teachersDTO.OrderByDescending(s=>s.StudentCount).Take(3);
        }
    }
}

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
        UnitOfWork uow;
        public TeacherService()
        {
            if (uow == null)
                uow = new UnitOfWork();
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
            var map = new MapperConfiguration(c => c.CreateMap<Teacher, TeacherDTO>()).CreateMapper();
            return map.Map<IEnumerable<Teacher>, IEnumerable<TeacherDTO>>(uow.Teachers.GetAll());
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
    }
}

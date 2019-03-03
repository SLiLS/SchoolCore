using System;
using System.Collections.Generic;
using System.Text;
using School.BLL.Intefaces;
using School.BLL.DTO;
using School.DAL.Repositories;
using School.DAL.Entities;
using AutoMapper;
using System.Linq;

namespace School.BLL.Services
{
   public class SchoolClassService : ISchoolClassService
    {
        UnitOfWork uow;
        public SchoolClassService()
        {
            if (uow == null)
                uow = new UnitOfWork();
        }
        public IEnumerable<SchoolClassDTO> GetAll()
        {
           
            var map = new MapperConfiguration(c => c.CreateMap<SchoolClass, SchoolClassDTO>()).CreateMapper();
            return map.Map<IEnumerable<SchoolClass>, IEnumerable<SchoolClassDTO>>(uow.SchoolClasses.GetAll());
        }
    }
}

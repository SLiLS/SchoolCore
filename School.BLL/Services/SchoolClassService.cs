using System;
using System.Collections.Generic;
using System.Text;
using School.BLL.Intefaces;
using School.BLL.DTO;
using School.DAL.Repositories;
using School.DAL.Entities;
using AutoMapper;
using School.DAL.Interfaces;
using System.Linq;

namespace School.BLL.Services
{
   public class SchoolClassService : ISchoolClassService
    {
        IUnitOfWork uow { get; set; }
        public SchoolClassService(IUnitOfWork unitOfWork)
        {

            uow = unitOfWork;
        }
        public IEnumerable<SchoolClassDTO> GetAll()
        {

            //var map = new MapperConfiguration(c => c.CreateMap<SchoolClass, SchoolClassDTO>().ForMember(dt=>dt.StudentCount,s=>s.MapFrom(a=>a.Students.Count))).CreateMapper();
            //return map.Map<IEnumerable<SchoolClass>, IEnumerable<SchoolClassDTO>>(uow.SchoolClasses.GetAll());
            List<SchoolClassDTO> ListDTO = new List<SchoolClassDTO>();
            List<SchoolClass> list = uow.SchoolClasses.GetAll().ToList();
            foreach (SchoolClass item in list)
            {
                SchoolClassDTO schoolClassDTO = new SchoolClassDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    StudentCount = uow.SchoolClasses.StudentCount(item.Id)
                };
                ListDTO.Add(schoolClassDTO);
            }
            return ListDTO;
        }

    }
}

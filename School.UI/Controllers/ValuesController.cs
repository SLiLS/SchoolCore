using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.BLL.DTO;
using School.UI.ViewModels;
using AutoMapper;
using School.BLL.Services;

namespace School.UI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        StudentService studentService;
        public ValuesController()
        {
            if (studentService == null)
                studentService = new StudentService();

        }

        // GET api/values
        [HttpGet]
        public IEnumerable<StudentViewModel> GetStudent()
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<StudentDTO, StudentViewModel>()).CreateMapper();
            return map.Map<IEnumerable<StudentDTO>,IEnumerable<StudentViewModel>>(studentService.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public StudentViewModel GetStudent(int id)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<StudentDTO, StudentViewModel>()).CreateMapper();
            return map.Map<StudentDTO,StudentViewModel>(studentService.Get(id));
        }

        // POST api/values
        [HttpPost]
        public void PostStudent([FromBody]StudentViewModel value)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<StudentViewModel, StudentDTO>()).CreateMapper();
            StudentDTO student= map.Map<StudentViewModel, StudentDTO>(value);
            studentService.Create(student);
        }
        
        public IEnumerable<StudentViewModel> GetStudentSearch(int? schoolclass, string sex)
        {
            var map = new MapperConfiguration(c => c.CreateMap<StudentDTO, StudentViewModel>()).CreateMapper();

            return map.Map<IEnumerable<StudentDTO>, IEnumerable<StudentViewModel>>(studentService.Search(schoolclass, sex));
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public void PutStudet(int id, [FromBody]StudentViewModel value)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<StudentViewModel, StudentDTO>()).CreateMapper();
            StudentDTO student = map.Map<StudentViewModel, StudentDTO>(value);
            studentService.Update(student);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void DeleteStudent(int id)
        {
            studentService.Delete(id);
        }
    }
}

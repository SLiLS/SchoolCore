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
        ServiceCreator serviceCreator;
        public ValuesController()
        {
            if (serviceCreator == null)
                serviceCreator = new ServiceCreator();
            
            

        }

        // GET api/values
        [HttpGet]
        public IEnumerable<StudentViewModel> GetStudent()
        {
            var map = new MapperConfiguration(c => c.CreateMap<StudentDTO, StudentViewModel>()).CreateMapper();
            
            return map.Map<IEnumerable<StudentDTO>, IEnumerable<StudentViewModel>>(serviceCreator.studentService().GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public StudentViewModel GetStudent(int id)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<StudentDTO, StudentViewModel>()).CreateMapper();
            return map.Map<StudentDTO,StudentViewModel>(serviceCreator.studentService().Get(id));
        }

        [HttpGet("{sex}/{schoolClass}")]
        public IEnumerable<StudentViewModel> GetStudentSeacrh(string sex,string schoolClass)
        {
            var map = new MapperConfiguration(c => c.CreateMap<StudentDTO, StudentViewModel>()).CreateMapper();

            var a = map.Map<IEnumerable<StudentDTO>, IEnumerable<StudentViewModel>>(serviceCreator.studentService().Search(sex,schoolClass));
            return a;
        }

        // POST api/values
        [HttpPost]
        public IActionResult PostStudent([FromBody]StudentViewModel value)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<StudentViewModel, StudentDTO>()).CreateMapper();
            StudentDTO student= map.Map<StudentViewModel, StudentDTO>(value);
            serviceCreator.studentService().Create(student);
            return Ok(value);
        }
        
     
        // PUT api/values/5
        [HttpPut]
        public IActionResult PutStudent( [FromBody]StudentViewModel value)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<StudentViewModel, StudentDTO>()).CreateMapper();
            StudentDTO student = map.Map<StudentViewModel, StudentDTO>(value);
            serviceCreator.studentService().Update(student);
            return Ok(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
          var student=  serviceCreator.studentService().Get(id);
            serviceCreator.studentService().Delete(id);
            return Ok(student);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.BLL.DTO;
using School.UI.ViewModels;
using AutoMapper;
using School.BLL.Services;


namespace School.UI.Controllers
{
    [Produces("application/json")]
    [Route("api/Teacher")]
    public class TeacherController : Controller
    {
        ServiceCreator serviceCreator;
        public TeacherController()
        {
            if (serviceCreator == null)
                serviceCreator = new ServiceCreator();



        }
        // GET: api/Teacher
        [HttpGet]
        public IEnumerable<TeacherViewModel> GetTeachers()
        {
          
            var map = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, TeacherViewModel>()).CreateMapper();
            return map.Map<IEnumerable<TeacherDTO>, IEnumerable<TeacherViewModel>>(serviceCreator.teacherService().GetAll());

        }
      

        //GET: api/Teacher/5
        [HttpGet("{id}", Name = "Get")]
        public TeacherViewModel GetTeacher(int id)
        {
            
            var map = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, TeacherViewModel>()).CreateMapper();
            return map.Map<TeacherDTO, TeacherViewModel>(serviceCreator.teacherService().Get(id));
        }

        // POST: api/Teacher
        [HttpPost]
        public IActionResult PostTeacher([FromBody]TeacherViewModel value)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<TeacherViewModel, TeacherDTO>()).CreateMapper();
            TeacherDTO teacher = map.Map<TeacherViewModel, TeacherDTO>(value);
            serviceCreator.teacherService().Create(teacher);
            return Ok(value);
        }

        // PUT: api/Teacher/5
        [HttpPut("{id}")]
        public IActionResult PutTeacher(int id, [FromBody]TeacherViewModel value)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<TeacherViewModel, TeacherDTO>()).CreateMapper();
            TeacherDTO teacher = map.Map<TeacherViewModel, TeacherDTO>(value);
            serviceCreator.teacherService().Update(teacher);
            return Ok(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var dTO = serviceCreator.teacherService().Get(id);
            serviceCreator.teacherService().Delete(id);
            return Ok(dTO);
        }
    }
}

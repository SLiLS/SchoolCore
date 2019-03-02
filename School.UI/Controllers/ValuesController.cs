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
        public IEnumerable<StudentViewModel> Get()
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<StudentDTO, StudentViewModel>()).CreateMapper();
            return map.Map<IEnumerable<StudentDTO>,IEnumerable<StudentViewModel>>(studentService.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

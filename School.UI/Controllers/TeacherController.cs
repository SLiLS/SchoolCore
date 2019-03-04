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
        public IEnumerable<TeacherViewModel> Get()
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, TeacherViewModel>()).CreateMapper();
            return map.Map<IEnumerable<TeacherDTO>, IEnumerable<TeacherViewModel>>(serviceCreator.teacherService().GetAll());

        }

        //// GET: api/Teacher/5
        ////[HttpGet("{id}", Name = "Get")]
        ////public string Get(int id)
        ////{
        ////    return "value";
        ////}

        //// POST: api/Teacher
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Teacher/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

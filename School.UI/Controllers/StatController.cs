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
    [Route("api/Stat")]
    public class StatController : Controller
    {

        ServiceCreator serviceCreator;
        public StatController()
        {
            if (serviceCreator == null)
                serviceCreator = new ServiceCreator();
        }
        // GET: api/Stat
        [HttpGet]
        public IEnumerable<TeacherViewModel> GetTop()
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, TeacherViewModel>()).CreateMapper();
            return map.Map<IEnumerable<TeacherDTO>, IEnumerable<TeacherViewModel>>(serviceCreator.teacherService().GetTop3());

        }


    }
}

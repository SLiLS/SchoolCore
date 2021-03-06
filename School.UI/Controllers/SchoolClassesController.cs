﻿using System;
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
    [Route("api/SchoolClasses")]
    public class SchoolClassesController : Controller
    {
        ServiceCreator serviceCreator;
        public SchoolClassesController()
        {
            if (serviceCreator == null)
                serviceCreator = new ServiceCreator();
        }
        // GET: api/SchoolClasses
        [HttpGet]
        public IEnumerable<SchoolClassViewModel> GetSchoolClass()
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<SchoolClassDTO, SchoolClassViewModel>()).CreateMapper();
            return map.Map<IEnumerable<SchoolClassDTO>, IEnumerable<SchoolClassViewModel>>(serviceCreator.schoolClassService().GetAll());

        }

     
    
    }
}

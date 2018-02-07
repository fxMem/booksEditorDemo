using BookEditorDemo.Models;
using BookEditorDemo.Web.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BookEditorDemo.Web.Controllers
{
    public class ControllerBase: ApiController
    {
        // Using property injection so 
        // sub-controllers wouldt have to pass constructor parameters around
        [Inject]
        public UserService UserService { get; set; }

        public UserSettings Settings
        {
            get
            {
                return UserService.Settings;
            }
        }

        
        
    }
}
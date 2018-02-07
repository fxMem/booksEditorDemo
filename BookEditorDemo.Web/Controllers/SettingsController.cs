using BookEditorDemo.Models;
using BookEditorDemo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BookEditorDemo.Web.Controllers
{
    public class SettingsController: ControllerBase
    {
        public UserSettings Get()
        {
            return Settings;
        }

        public void Post([FromBody]SettingsData settings)
        {
            UserService.SetSortingProperty(settings.SortProperty);
        }
    }
}
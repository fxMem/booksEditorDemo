using BookEditorDemo;
using BookEditorDemo.Models;
using BookEditorDemo.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BookEditorDemo.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            InitializeNinject();
            InitializeDemoState();

            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void InitializeNinject()
        {
            var resolver = new NinjectInitializer().GetResolver();

            // ASP.NET MVC
            DependencyResolver.SetResolver(resolver);

            // WebAPI
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        private void InitializeDemoState()
        {
            var demoBooks = (DemoBooksHelper)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(DemoBooksHelper));
            demoBooks.AddDemoBooks();

            var fileRepository = (SimpleFileRepository)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IFilesRepository));
            fileRepository.SetFileDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data"));
        }
    }
}

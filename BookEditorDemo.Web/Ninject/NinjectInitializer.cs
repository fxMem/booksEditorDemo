using BookEditorDemo.Models;
using BookEditorDemo.Web.Controllers;
using BookEditorDemo.Web.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookEditorDemo
{
    public class NinjectInitializer
    {
        public NinjectResolver GetResolver()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IBooksRepository>().To<InMemoryBooksRepository>().InSingletonScope();
            kernel.Bind<IAuthorsRepository>().To<InMemoryAuthosRepository>().InSingletonScope();
            kernel.Bind<IFilesRepository>().To<SimpleFileRepository>().InSingletonScope();
            kernel.Bind<IUserSettingsRepository>().To<InMemorySettingsRepository>().InSingletonScope();
            kernel.Bind<BooksService>().ToSelf().InTransientScope();
            kernel.Bind<UserService>().ToSelf().InTransientScope();
            //kernel.Bind<BooksController>().ToSelf().InTransientScope();

            var ninjectResolver = new NinjectResolver(kernel);
            return ninjectResolver;
        }
    }
}
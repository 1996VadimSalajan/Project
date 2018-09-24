using Autofac;
using Autofac.Core;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.Mvc;
using CodeFirst;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Controllers;
using WebApplication3.Models;

namespace WebApplication3.Autofac
{
    public class AutofacConfig
    {


        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly)
                .AsImplementedInterfaces();
            builder.RegisterModule(new AutofacWebTypesModule());
            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>();
            builder.Register(a=> HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()).As<ApplicationUserManager>();
            builder.Register(a=> HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser>>()
                .WithParameter(new ResolvedParameter(
                       (pi, ctx) => pi.ParameterType == typeof(DbContext),
                       (pi, ctx) => ctx.Resolve<ApplicationDbContext>()));
            builder.RegisterType<UserManager<ApplicationUser>>();
            builder.RegisterGeneric(typeof(Repository<>))
            .As(typeof(IRepository<>))
            .InstancePerDependency();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
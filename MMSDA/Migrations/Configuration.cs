namespace MMSDA.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using Global;

    internal sealed class Configuration : DbMigrationsConfiguration<MMSDA.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MMSDA.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            SeedAdminUser(context);
        }

        private void SeedAdminUser(ApplicationDbContext context)
        {
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleMgr = new RoleManager<IdentityRole>(roleStore);
            if (!roleMgr.RoleExists(WebConfig.adminRole))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = WebConfig.adminRole });
            }
            //var userStore = new UserStore<ApplicationUser> (context);
            //var userMgr = new UserManager<ApplicationUser> (userStore);
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                UserName = WebConfig.adminName,
                Email = WebConfig.adminEmail
            };
            IdUserResult = userMgr.Create(appUser, WebConfig.adminPassword);
            if (!userMgr.IsInRole(userMgr.FindByEmail(WebConfig.adminEmail).Id, WebConfig.adminRole))
            {
                // IdUserResult = userMgr.AddToRole(appUser.Id, "canEdit");
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail(WebConfig.adminEmail).Id, WebConfig.adminRole);
            }
        }
    }
}

using Books__App.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Books__App.Startup))]
namespace Books__App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            var context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("Admin"))
            {
                var adminRole = new IdentityRole
                {
                    Name = "Admin",
                };
                roleManager.Create(adminRole);

                var adminUser = new ApplicationUser
                {
                    UserName = "Bahaa@Sabis.com",
                    Email = "Bahaa@Sabis.com",
                };
                var password = "P@ssw0rd";
               var result = userManager.Create(adminUser, password);
               //checking if the user added or not 
               if (result.Succeeded)
               {
                   userManager.AddToRole(adminUser.Id, "Admin");
               }
            }
        }
    }
}

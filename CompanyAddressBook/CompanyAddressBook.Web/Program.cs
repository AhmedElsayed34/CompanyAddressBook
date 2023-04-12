using CompanyAddressBook.Business.Services;
using CompanyAddressBook.Core.Entities.Business.Repositories;
using CompanyAddressBook.Core.Interfaces;
using CompanyAddressBook.Data;
using CompanyAddressBook.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CompanyAddressBook.Web.Mappings;

namespace CompanyAddressBook.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped< ICompanyService, CompanyService>();
            builder.Services.AddScoped< IContactService, ContactService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddLogging(configure => configure.AddConsole());
            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Company}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
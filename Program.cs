using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_Music.Data;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Repositories;
using UBB_SE_2024_Music.Services;

namespace UBB_SE_2024_Music
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IRepository<Song>, Repository<Song>>();
            builder.Services.AddScoped<IRepository<SongFeatures>, Repository<SongFeatures>>();
            builder.Services.AddScoped<IRepository<SongRecommendationDetails>, Repository<SongRecommendationDetails>>();
            builder.Services.AddScoped<IRepository<ArtistDetails>, Repository<ArtistDetails>>();
            builder.Services.AddScoped<ArtistDashboardService>();

            builder.Services.AddScoped<IRecapService, RecapService>();
            builder.Services.AddScoped<ISongBasicDetailsRepository, SongRepository>();
            builder.Services.AddScoped<IUserPlaybackBehaviourRepository, UserPlaybackBehaviourRepository>();

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IUserService, UserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}

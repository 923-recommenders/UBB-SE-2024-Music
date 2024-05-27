using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_Music.Data;
using UBB_SE_2024_Music.Mappings;
using UBB_SE_2024_Music.Repositories.Interfaces;
using UBB_SE_2024_Music.Repositories;
using UBB_SE_2024_Music.Services.Interfaces;
using UBB_SE_2024_Music.Models;
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
                options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IRepository<Song>, Repository<Song>>();
            builder.Services.AddScoped<IRepository<SongFeatures>, Repository<SongFeatures>>();
            builder.Services.AddScoped<IRepository<SongRecommendationDetails>, Repository<SongRecommendationDetails>>();
            builder.Services.AddScoped<IRepository<ArtistDetails>, Repository<ArtistDetails>>();
            builder.Services.AddScoped<ArtistDashboardService>();

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IUserService, UserService>();

            builder.Services.AddScoped<IRecapService, RecapService>();
            builder.Services.AddScoped<ISongBasicDetailsRepository, SongRepository>();
            builder.Services.AddScoped<IUserPlaybackBehaviourRepository, UserPlaybackBehaviourRepository>();

            // Inject automappers
            builder.Services.AddAutoMapper(typeof(SoundMappingProfile));
            builder.Services.AddAutoMapper(typeof(SongMappingProfile));
            builder.Services.AddAutoMapper(typeof(PlaylistMappingProfile));

            // Inject repositories
            builder.Services.AddSingleton<ISoundRepository, SoundRepository>();
            builder.Services.AddSingleton<IPlaylistRepository, PlaylistRepository>();
            builder.Services.AddSingleton<IPlaylistSongItemRepository, PlaylistSongItemRepository>();
            builder.Services.AddSingleton<ICreationRepository, CreationRepository>();
            builder.Services.AddSingleton<ISongRepository, SongRepository>();

            // Inject services
            builder.Services.AddSingleton<ISoundService, SoundService>();
            builder.Services.AddSingleton<ISongService, SongService>();
            builder.Services.AddSingleton<IPlaylistService, PlaylistService>();
            builder.Services.AddSingleton<IPlaylistSongItemService, PlaylistSongItemService>();
            builder.Services.AddSingleton<ICreationService, CreationService>();
            builder.Services.AddSingleton<IRepository<Song>, Repository<Song>>();
            builder.Services.AddSingleton<IRepository<SongFeatures>, Repository<SongFeatures>>();
            builder.Services.AddSingleton<IRepository<SongRecommendationDetails>, Repository<SongRecommendationDetails>>();
            builder.Services.AddSingleton<IRepository<ArtistDetails>, Repository<ArtistDetails>>();
            builder.Services.AddSingleton<ArtistDashboardService>();

            builder.Services.AddSingleton<IRecapService, RecapService>();
            builder.Services.AddSingleton<ISongBasicDetailsRepository, SongRepository>();
            builder.Services.AddSingleton<IUserPlaybackBehaviourRepository, UserPlaybackBehaviourRepository>();

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

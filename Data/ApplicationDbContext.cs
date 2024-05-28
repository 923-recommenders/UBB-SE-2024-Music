using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ArtistDetails> ArtistDetails { get; set; }
        public DbSet<AdDistributionData> AdDistributionData { get; set; }
        public DbSet<SongRecommendationDetails> SongRecommendationDetails { get; set; }
        public DbSet<UserPlaybackBehaviour> UserPlaybackBehaviour { get; set; }
        public DbSet<SongFeatures> SongFeatures { get; set; }
        public DbSet<Trends> Trends { get; set; }
        public DbSet<UserDemographicsDetails> UserDemographicsDetails { get; set; }
        public DbSet<MostPlayedArtistInformation> MostPlayedArtistInformation { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ExcludedCountry> ExcludedCountries { get; set; }
        public DbSet<Sound> Sounds { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistSongItem> PlaylistSongItems { get; set; }

        public virtual DbSet<Creation> Creations { get; set; }
        public virtual DbSet<CreationSoundItem> CreationSoundItems { get; set; }
    }
}

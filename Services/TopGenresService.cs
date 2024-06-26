﻿using UBB_SE_2024_Music.DTO;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Repositories;

namespace UBB_SE_2024_Music.Services
{
    public class TopGenresService : ITopGenresService
    {
        private readonly IRepository<Song> songRepo;
        private readonly IRepository<SongRecommendationDetails> songRecommendationRepo;

        public TopGenresService(IRepository<Song> songRepo, IRepository<SongRecommendationDetails> songRecommendationRepo)
        {
            this.songRepo = songRepo;
            this.songRecommendationRepo = songRecommendationRepo;
        }

        /// <summary>
        /// Retrieves the top 3 genres for a specified month and year as a list of GenreData objects.
        /// </summary>
        public async Task<List<GenreData>> GetTop3Genres(int month, int year)
        {
            int totalMinutes = 0;
            Dictionary<string, int> genreCount = new Dictionary<string, int>();
            var songs = await songRepo.GetAll();
            var songDetails = await songRecommendationRepo.GetAll();

            foreach (var song in songs)
            {
                foreach (var songDetail in songDetails)
                {
                    if (songDetail.SongId == song.SongId && songDetail.Month == month && songDetail.Year == year)
                    {
                        totalMinutes += songDetail.MinutesListened;
                        if (genreCount.ContainsKey(song.Genre))
                        {
                            genreCount[song.Genre] += songDetail.MinutesListened;
                        }
                        else
                        {
                            genreCount.Add(song.Genre, songDetail.MinutesListened);
                        }
                    }
                }
            }

            var sortedGenres = from entry in genreCount orderby entry.Value descending select entry;
            List<GenreData> result = new List<GenreData>();
            int count = 0;
            foreach (KeyValuePair<string, int> entry in sortedGenres)
            {
                result.Add(new GenreData(entry.Key, entry.Value, (entry.Value * 100.0 / totalMinutes)));
                if (count == 2)
                {
                    break;
                }
                count++;
            }
            return result;
        }

        /// <summary>
        /// Retrieves the top 3 subgenres for a specified month and year as a list of GenreData objects.
        /// </summary>
        public async Task<List<GenreData>> GetTop3SubGenres(int month, int year)
        {
            Dictionary<string, int> subgenreCount = new Dictionary<string, int>();
            int totalMinutes = 0;
            var songs = await songRepo.GetAll();
            var songDetails = await songRecommendationRepo.GetAll();

            foreach (var song in songs)
            {
                foreach (var songDetail in songDetails)
                {
                    if (songDetail.SongId == song.SongId && songDetail.Month == month && songDetail.Year == year)
                    {
                        totalMinutes += songDetail.MinutesListened;
                        if (subgenreCount.ContainsKey(song.Subgenre))
                        {
                            subgenreCount[song.Subgenre] += songDetail.MinutesListened;
                        }
                        else
                        {
                            subgenreCount.Add(song.Subgenre, songDetail.MinutesListened);
                        }
                    }
                }
            }

            var sortedSubGenres = from entry in subgenreCount orderby entry.Value descending select entry;
            List<GenreData> result = new List<GenreData>();
            int count = 0;
            foreach (KeyValuePair<string, int> entry in sortedSubGenres)
            {
                result.Add(new GenreData(entry.Key, entry.Value, entry.Value * 100.0 / totalMinutes));
                if (count == 2)
                {
                    break;
                }
                count++;
            }
            return result;
        }
    }
}

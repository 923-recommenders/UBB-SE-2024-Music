﻿namespace UBB_SE_2024_Music.Models
{
    public class PlaylistForAddUpdateModel
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}

namespace UBB_SE_2024_Music.DTO
{
    public class GenreData
    {
        public string Name { get; set; }
        public int Minutes { get; set; }
        public double Percentage { get; set; }

        public GenreData(string name, int minutes, double percentage)
        {
            Name = name;
            Minutes = minutes;
            Percentage = percentage;
        }
    }
}

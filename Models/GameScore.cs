

using Microsoft.EntityFrameworkCore;

namespace SlideMasters_BlazorApp.Models
{
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

    [PrimaryKey("ID")]
    public class GameScore
    {
        public uint ID { get; set; }
        public string Username { get; set; }
        public int Score { get; set; }

        public DifficultyLevel Difficulty { get; set; }
        public GameScore(string username, int score, DifficultyLevel difficulty)
        {
            Username = username;
            Score = score;
            Difficulty = difficulty;
        }
    }
}

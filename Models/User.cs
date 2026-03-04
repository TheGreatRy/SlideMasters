using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace SlideMasters_BlazorApp.Models
{
    [PrimaryKey("ID")]
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public List<GameScore> GameScores { get; set; } = new List<GameScore>();

        public User(string username)
        {
            Username = username;
        }

        /// <summary>
        /// Replaces the best score if the new score is better (lower time) than the existing one for the same difficulty level. <br/>
        /// If no score exists for that difficulty, it adds the new score.
        /// </summary>
        /// <param name="newScore">The new score to be considered for replacement.</param>
        public void ReplaceHighScore(GameScore newScore)
        {
            var existingScore = GameScores.FirstOrDefault(s => s.Difficulty == newScore.Difficulty);

            if (existingScore == null)
            {
                GameScores.Add(newScore);
            }
            else if (newScore.Score < existingScore.Score) // Lower time is better
            {
                GameScores.Remove(existingScore);
                GameScores.Add(newScore);
            }
        }
    }
}

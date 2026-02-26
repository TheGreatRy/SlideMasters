using System.Security.Cryptography.X509Certificates;

namespace SlideMasters_BlazorApp.Models
{
    public class User
    {
        public string Username { get; set; }
        public List<GameScore> GameScores { get; set; } = new List<GameScore>();

        public User(string username)
        {
            Username = username;
        }

        /// <summary>
        /// Replaces the highest score if the new score is higher than the existing one for the same difficulty level. <br/>
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
            else if (newScore.Score > existingScore.Score)
            {
                GameScores.Remove(existingScore);
                GameScores.Add(newScore);
            }
        }


    }
}

using Microsoft.EntityFrameworkCore;

namespace SlideMasters_BlazorApp.Models
{
    public class DBContextService : DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<GameScore> GameScore { get; set; }

        public DBContextService(DbContextOptions<DBContextService> options) : base(options)
        {
        }

        public List<GameScore> GetHighScoresByDifficulty(DifficultyLevel difficultyLevel)
        {
            return GameScore.Where(s => s.Difficulty == difficultyLevel)
                           .OrderBy(s => s.Score) // Lower time is better
                           .ToList();
        }

        public void AddNewScore(GameScore newScore)
        {
            var user = Users.FirstOrDefault(u => u.Username == newScore.Username);
            if (user == null)
            {
                user = new User(newScore.Username);
                Users.Add(user);
            }
            
            GameScore.Add(newScore);
            user.ReplaceHighScore(newScore);
            SaveChanges();
        }
        
        public List<GameScore> GetAllScores()
        {
            return GameScore.OrderBy(s => s.Score) // Lower time is better
                           .ToList();
        }
    }
}

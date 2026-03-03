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

        public GameScore GetEasyHighScore()
        {
            return GameScore.FirstOrDefault(s => s.Difficulty == DifficultyLevel.Easy)!;
        }

        public GameScore GetMediumHighScore()
        {
            return GameScore.FirstOrDefault(s => s.Difficulty == DifficultyLevel.Medium)!;
        }

        public GameScore GetHardHighScore()
        {
            return GameScore.FirstOrDefault(s => s.Difficulty == DifficultyLevel.Hard)!;

        }

        public void AddNewScore(GameScore newScore)
        {
            var user = Users.FirstOrDefault(u => u.Username == newScore.Username);
            if (user == null)
            {
                user = new User(newScore.Username);
                Users.Add(user);
            }
            user.ReplaceHighScore(newScore);
            SaveChanges();
        }
        
        public List<GameScore> GetAllScores()
        {
            return GameScore.ToList();
        }
    }
}

namespace SlideMasters_BlazorApp.Models
{

    public enum GameState
    {
        Title,
        InProgress,
        GameOver
    }
    public class GameController
    {
        public List<ImageBlock> slidingPuzzleBlocks = new List<ImageBlock>();
        public GameState CurrentState { get; private set; }
        public GameController()
        {
            CurrentState = GameState.Title;
        }
        
        public void ChangeState(GameState newState)
        {
            if (CurrentState != newState)
            {
                CurrentState = newState;
            }
        }


        public void CanMove()
        {

        }
    }
}
            
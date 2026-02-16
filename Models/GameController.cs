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

        public bool IsCompleted()
        {
            for(int i = 0; i < slidingPuzzleBlocks.Count; i++)
            {
                var block = slidingPuzzleBlocks[i];
                if (block.BoardX != i % 4 || block.BoardY != i / 4)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
            
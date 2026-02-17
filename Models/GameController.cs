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

        /// <summary>
        /// Checks if a move is valid based on the current position of the empty space and the block to be moved.
        /// </summary>
        public void CanMove()
        {

        }

        /// <summary>
        /// Checks if the puzzle is completed.
        /// </summary>
        /// <returns>True if the puzzle is in order, False otherwise</returns>
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
            
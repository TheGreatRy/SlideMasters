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
        /// Checks if the puzzle is completed.
        /// </summary>
        /// <returns>True if the puzzle is in order, False otherwise</returns>
        public bool IsCompleted()
        {
            for(int j = 0; j < slidingPuzzleBlocks.Count; j++)
                {
                var block = slidingPuzzleBlocks[j];

                if (block.BoardID != j)
                {
                    return false;
                }
            }
            return true;
        }

        public void SwapBlocks(int firstIndex, int secondIndex)
        {
            //store original board positions
            var firstX = slidingPuzzleBlocks[firstIndex].BoardX;
            var firstY = slidingPuzzleBlocks[firstIndex].BoardY;
            
            var secondX = slidingPuzzleBlocks[secondIndex].BoardX;
            var secondY = slidingPuzzleBlocks[secondIndex].BoardY;

            //Swap blocks
            var temp = slidingPuzzleBlocks[firstIndex];

            slidingPuzzleBlocks[firstIndex] = slidingPuzzleBlocks[secondIndex];
            slidingPuzzleBlocks[secondIndex] = temp;

            //Since the swap includes the board x and y, use stored values to correct position
            slidingPuzzleBlocks[firstIndex].BoardX = firstX;
            slidingPuzzleBlocks[firstIndex].BoardY = firstY;

            slidingPuzzleBlocks[secondIndex].BoardX = secondX;
            slidingPuzzleBlocks[secondIndex].BoardY = secondY;
        }
    }
}
            
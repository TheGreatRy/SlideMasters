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
            //we need to update the image data FIRST then update the board positions
            var storeFirst = slidingPuzzleBlocks[firstIndex];
            var storeSecond = slidingPuzzleBlocks[secondIndex];

            slidingPuzzleBlocks[firstIndex] = storeSecond;
            slidingPuzzleBlocks[firstIndex].BoardX = storeFirst.BoardX;
            slidingPuzzleBlocks[firstIndex].BoardY = storeFirst.BoardY;

            slidingPuzzleBlocks[secondIndex] = storeFirst;
            slidingPuzzleBlocks[secondIndex].BoardX = storeSecond.BoardX;
            slidingPuzzleBlocks[secondIndex].BoardY = storeSecond.BoardY;

        }
    }
}
            
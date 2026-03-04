namespace SlideMasters_BlazorApp.Models
{
    public static class ImageRandomizer
    {
        private static Random _random = new Random();
        
        public static void Shuffle<T>(ref List<T> list)
        {
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = _random.Next(0, i + 1);
                // Swap list[i] with list[j]
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        /// <summary>
        /// Shuffles a sliding puzzle ensuring it remains solvable.
        /// Based on the mathematical rules for sliding puzzle solvability.
        /// </summary>
        /// <param name="blocks">List of puzzle blocks to shuffle</param>
        /// <param name="gridWidth">Width of the puzzle grid</param>
        public static void ShufflePuzzle(List<ImageBlock> blocks, int gridWidth)
        {
            // Find the empty block index
            int emptyIndex = -1;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].IsEmptyBlock)
                {
                    emptyIndex = i;
                    break;
                }
            }

            if (emptyIndex == -1) return; // No empty block found

            // Create array of BoardIDs for easier manipulation (excluding empty block)
            List<int> boardIds = new List<int>();
            for (int i = 0; i < blocks.Count; i++)
            {
                if (!blocks[i].IsEmptyBlock)
                {
                    boardIds.Add(blocks[i].BoardID);
                }
            }

            // Shuffle until we get a solvable configuration
            do
            {
                // Shuffle the non-empty blocks
                for (int i = boardIds.Count - 1; i > 0; i--)
                {
                    int j = _random.Next(0, i + 1);
                    int temp = boardIds[i];
                    boardIds[i] = boardIds[j];
                    boardIds[j] = temp;
                }

                // Place empty block in random position
                emptyIndex = _random.Next(0, blocks.Count);

            } while (!IsSolvable(boardIds, emptyIndex, gridWidth));

            // Apply the solvable configuration to the blocks
            ApplyConfiguration(blocks, boardIds, emptyIndex);
        }

        /// <summary>
        /// Checks if a sliding puzzle configuration is solvable.
        /// Rules:
        /// - For odd grid width: solvable if inversion count is even
        /// - For even grid width: 
        ///   - If empty block is on even row from bottom: solvable if inversion count is odd
        ///   - If empty block is on odd row from bottom: solvable if inversion count is even
        /// </summary>
        private static bool IsSolvable(List<int> boardIds, int emptyIndex, int gridWidth)
        {
            int inversionCount = CountInversions(boardIds);

            if (gridWidth % 2 == 1)
            {
                // Odd grid width: solvable if inversion count is even
                return inversionCount % 2 == 0;
            }
            else
            {
                // Even grid width: consider empty block position
                int emptyRow = emptyIndex / gridWidth;
                int emptyRowFromBottom = gridWidth - emptyRow;

                if (emptyRowFromBottom % 2 == 0)
                {
                    // Empty block on even row from bottom: solvable if inversion count is odd
                    return inversionCount % 2 == 1;
                }
                else
                {
                    // Empty block on odd row from bottom: solvable if inversion count is even
                    return inversionCount % 2 == 0;
                }
            }
        }

        /// <summary>
        /// Counts the number of inversions in the puzzle.
        /// An inversion is when a larger numbered tile appears before a smaller numbered tile.
        /// </summary>
        private static int CountInversions(List<int> boardIds)
        {
            int inversionCount = 0;

            for (int i = 0; i < boardIds.Count; i++)
            {
                for (int j = i + 1; j < boardIds.Count; j++)
                {
                    if (boardIds[i] > boardIds[j])
                    {
                        inversionCount++;
                    }
                }
            }

            return inversionCount;
        }

        /// <summary>
        /// Applies the solved configuration to the actual blocks
        /// </summary>
        private static void ApplyConfiguration(List<ImageBlock> blocks, List<int> boardIds, int emptyIndex)
        {
            // Create a new ordered list
            var orderedBlocks = new List<ImageBlock>(blocks.Count);
            
            // Fill with null initially
            for (int i = 0; i < blocks.Count; i++)
            {
                orderedBlocks.Add(null);
            }

            // Place the empty block at its position
            ImageBlock emptyBlock = blocks.First(b => b.IsEmptyBlock);
            orderedBlocks[emptyIndex] = emptyBlock;

            // Place non-empty blocks according to boardIds
            int boardIdIndex = 0;
            for (int i = 0; i < orderedBlocks.Count; i++)
            {
                if (orderedBlocks[i] == null) // Not the empty block position
                {
                    int targetBoardId = boardIds[boardIdIndex];
                    ImageBlock targetBlock = blocks.First(b => !b.IsEmptyBlock && b.BoardID == targetBoardId);
                    orderedBlocks[i] = targetBlock;
                    boardIdIndex++;
                }
            }

            // Replace the original list with the ordered blocks
            blocks.Clear();
            blocks.AddRange(orderedBlocks);

            // Update board coordinates for all blocks
            int gridWidth = (int)Math.Sqrt(blocks.Count);
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].BoardX = i % gridWidth;
                blocks[i].BoardY = i / gridWidth;
            }

            // Debug output
            Console.WriteLine("Shuffle applied successfully!");
            Console.WriteLine($"Grid width: {gridWidth}, Total blocks: {blocks.Count}");
            for (int i = 0; i < blocks.Count; i++)
            {
                var block = blocks[i];
                Console.WriteLine($"Position {i}: BoardID={block.BoardID}, IsEmpty={block.IsEmptyBlock}, Coords=({block.BoardX},{block.BoardY})");
            }
        }

        /// <summary>
        /// Simple shuffle for testing - performs valid moves from solved state
        /// </summary>
        public static void SimpleShufflePuzzle(List<ImageBlock> blocks, int gridWidth, int moves = 1000)
        {
            Console.WriteLine("Performing simple shuffle with valid moves...");
            
            // Find empty block
            int emptyIndex = -1;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].IsEmptyBlock)
                {
                    emptyIndex = i;
                    break;
                }
            }

            if (emptyIndex == -1) return;

            // Perform random valid moves
            for (int move = 0; move < moves; move++)
            {
                List<int> validMoves = GetValidMoves(emptyIndex, gridWidth, blocks.Count);
                if (validMoves.Count > 0)
                {
                    int randomMove = validMoves[_random.Next(validMoves.Count)];
                    
                    // Swap empty block with the selected adjacent block
                    var temp = blocks[emptyIndex];
                    blocks[emptyIndex] = blocks[randomMove];
                    blocks[randomMove] = temp;

                    // Update board coordinates
                    blocks[emptyIndex].BoardX = emptyIndex % gridWidth;
                    blocks[emptyIndex].BoardY = emptyIndex / gridWidth;
                    blocks[randomMove].BoardX = randomMove % gridWidth;
                    blocks[randomMove].BoardY = randomMove / gridWidth;

                    emptyIndex = randomMove;
                }
            }

            Console.WriteLine($"Simple shuffle completed with {moves} moves");
        }

        /// <summary>
        /// Gets valid moves for the empty block (adjacent positions)
        /// </summary>
        private static List<int> GetValidMoves(int emptyIndex, int gridWidth, int totalBlocks)
        {
            List<int> validMoves = new List<int>();
            
            int row = emptyIndex / gridWidth;
            int col = emptyIndex % gridWidth;

            // Up
            if (row > 0)
                validMoves.Add((row - 1) * gridWidth + col);
            
            // Down
            if (row < gridWidth - 1)
                validMoves.Add((row + 1) * gridWidth + col);
            
            // Left
            if (col > 0)
                validMoves.Add(row * gridWidth + (col - 1));
            
            // Right
            if (col < gridWidth - 1)
                validMoves.Add(row * gridWidth + (col + 1));

            return validMoves;
        }
    }
}

using System.Collections.Generic;

namespace Tetris
{
    public abstract class Block
    {
        // Contains the tile positions of the 4 rotation states
        protected abstract Position[][] Tiles { get; }
        // Where the block starts on the grid
        protected abstract Position StartOffset { get; }
        // Unique ID to distinguish the blocks
        public abstract int Id { get; }

        // Current States
        private int rotationState;
        private Position offset;

        public Block()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
        }

        /*
         * Returns the grid positions occupied by the block
         * factoring in the current rotation and offset
         */
        public IEnumerable<Position> TilePositions()
        {
            foreach (Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        public void RotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        public void RotateCCW()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }

        public void Move(int rows, int columns)
        {
            offset.Row += rows;
            offset.Column += columns;
        }

        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    }
}

using System;
using UnityEngine;

namespace Project.Games.Stromkreis
{
    public abstract class Block : MonoBehaviour
    {
        public Block Up, Down, Left, Right;

        public abstract Direction[] GetPathsFor(Direction dir);

        public Block GetBlock(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return Up;
                case Direction.Right:
                    return Right;
                case Direction.Down:
                    return Down;
                case Direction.Left:
                    return Left;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }
    }

    public enum Direction
    {
        Up    = 1,
        Right  = 2,
        Down  = 3,
        Left = 4
    }
}

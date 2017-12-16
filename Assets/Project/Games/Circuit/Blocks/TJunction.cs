using System;
// ReSharper disable InconsistentNaming

namespace Project.Games.Circuit.Blocks
{
    public class TJunction : Block
    {
        public override Direction[] GetPathsFor(Direction dir)
        {
            switch (dir.RotateBy((int)transform.rotation.eulerAngles.z))
            {
                case Direction.Up:
                    return new[] { Direction.Right, Direction.Down };
                case Direction.Right:
                    return new[] { Direction.Up, Direction.Down };
                case Direction.Down:
                    return new[] { Direction.Up, Direction.Right };
                default:
                    return new Direction[0];
            }
        }
    }
}

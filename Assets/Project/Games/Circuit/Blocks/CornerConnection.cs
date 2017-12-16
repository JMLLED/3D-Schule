using System;

namespace Project.Games.Circuit.Blocks
{
    public class CornerConnection : Block
    {
        public override Direction[] GetPathsFor(Direction dir)
        {
            switch (dir.RotateBy((int)transform.rotation.eulerAngles.z))
            {
                case Direction.Up:
                    return new[] { Direction.Right };
                case Direction.Right:
                    return new[] { Direction.Up };
                default:
                    return new Direction[0];
            }
        }
    }
}

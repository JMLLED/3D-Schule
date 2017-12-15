using System;

namespace Project.Games.Circuit.Blocks
{
    public class StraightConnection : Block
    {
        public override Direction[] GetPathsFor(Direction dir)
        {
            dir = (Direction)Math.IEEERemainder((int)(dir + (int) (transform.rotation.eulerAngles.z / 90)), 4);
            switch (dir)
            {
                case Direction.Up:
                    return new[] { Direction.Down };
                case Direction.Down:
                    return new[] { Direction.Up };
                default:
                    return new Direction[0];
            }
        }
    }
}

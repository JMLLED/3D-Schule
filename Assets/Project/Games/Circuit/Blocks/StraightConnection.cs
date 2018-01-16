using System.Collections.Generic;

namespace Project.Games.Circuit.Blocks
{
    public class StraightConnection : Block
    {
        protected override IEnumerable<Direction> GetConnectionsFrom(Direction dir)
        {
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

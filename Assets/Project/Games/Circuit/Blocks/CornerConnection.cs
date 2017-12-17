using System;
using System.Collections.Generic;

namespace Project.Games.Circuit.Blocks
{
    public class CornerConnection : Block
    {
        protected override IEnumerable<Direction> GetConnectionsFrom(Direction dir)
        {
            switch (dir)
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

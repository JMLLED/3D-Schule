using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace Project.Games.Circuit.Blocks
{
    public class TJunction : Block
    {
        protected override IEnumerable<Direction> GetConnectionsFrom(Direction dir)
        {
            switch (dir)
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

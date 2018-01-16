using System.Collections.Generic;

namespace Project.Games.Circuit.Blocks
{
    public class Ground : Block
    {
        protected override IEnumerable<Direction> GetConnectionsFrom(Direction dir)
        {
            return dir == Direction.Up ? new[] {Direction.Up} : new Direction[0];
        }
    }
}

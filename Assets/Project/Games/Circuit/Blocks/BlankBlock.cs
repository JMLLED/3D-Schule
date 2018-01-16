using System.Collections.Generic;

namespace Project.Games.Circuit.Blocks
{
    public class BlankBlock : Block
    {
        protected override IEnumerable<Direction> GetConnectionsFrom(Direction dir) => new Direction[0];
    }
}

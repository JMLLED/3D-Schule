using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Games.Circuit.Blocks
{
    public class CrossJunction : Block
    {
        protected override IEnumerable<Direction> GetConnectionsFrom(Direction dir)
        {
            return ((Direction[])Enum.GetValues(typeof(Direction))).Except(new [] { dir }).ToArray();
        }
    }
}

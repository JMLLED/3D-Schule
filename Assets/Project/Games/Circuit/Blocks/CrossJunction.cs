using System;
using System.Linq;

namespace Project.Games.Circuit.Blocks
{
    public class CrossJunction : Block
    {
        public override Direction[] GetPathsFor(Direction dir)
        {
            return ((Direction[])Enum.GetValues(typeof(Direction))).Except(new [] { dir }).ToArray();
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Project.Games.Circuit.Blocks
{
    public class Power : Block
    {
        public override Direction[] GetPathsFor(Direction dir) => new Direction[0];

        public bool FindPath()
        {
            var up = Direction.Up.RotateBy((int)transform.rotation.eulerAngles.z);
            var path = FindPath(up, GetBlock(up));
            path.ForEach(block => block.Lit = true);
            return path != null;
        }

        private List<Block> FindPath(Direction dir, Block block)
        {
            var otherDirections = block.GetPathsFor(dir);
            var otherBlocks = otherDirections.Select(block.GetBlock).ToArray();
            var possiblePaths = new List<Block>[otherBlocks.Length];
            for (var i = 0; i < otherBlocks.Length; i++)
            {
                possiblePaths[i] = FindPath(otherDirections[i].Invert(), otherBlocks[i]);
            }
            return possiblePaths.Aggregate((list1, list2) => list1?.Count < list2?.Count ? list1 : list2);
        }
    }
}

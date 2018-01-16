using System.Collections.Generic;
using System.Linq;

namespace Project.Games.Circuit.Blocks
{
    public class Power : Block
    {
        protected override IEnumerable<Direction> GetConnectionsFrom(Direction dir) => new Direction[0];

        private List<Block> path;

        public bool FindPath()
        {
            path?.ForEach(block => block.Lit = false);

            var up = Direction.Up.RotateBy((int)-transform.rotation.eulerAngles.z);
            var upBlock = GetBlock(up);

            path = FindPath(up.Invert(), upBlock, new List<Block> { this, upBlock });
            path?.ForEach(block => block.Lit = true);

            return path != null;
        }

        private static List<Block> FindPath(Direction dir, Block block, List<Block> alreadyVisited)
        {
            if (block == null)
                return null;

            //Falls der Block die Erde ist
            if (block is Ground && block.GetPossiblePaths(dir).Contains(dir))
                return alreadyVisited; // Gib den weg zurück

            var possibleDirs = block.GetPossiblePaths(dir); //Sonst suche nach anderen Wegen
            var possibleBlocks = possibleDirs.Where(possibleDir => block.GetBlock(possibleDir) != null).ToDictionary(block.GetBlock, direction => direction);
            alreadyVisited.ForEach(visitedBlock => possibleBlocks.Remove(visitedBlock));

            var possiblePaths = possibleBlocks.Keys.Select(possibleBlock =>
            {
                var newAlreadyVisited = new List<Block>(alreadyVisited) {possibleBlock};
                return FindPath(possibleBlocks[possibleBlock].Invert(), possibleBlock, newAlreadyVisited);
            }).Where(possiblePath => possiblePath != null);

            return possiblePaths.OrderBy(list => list.Count).FirstOrDefault();
        }
    }
}

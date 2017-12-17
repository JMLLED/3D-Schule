using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Games.Circuit.Blocks
{
    public class Power : Block
    {
        protected override IEnumerable<Direction> GetConnectionsFrom(Direction dir) => new Direction[0];

        private List<Block> path;

        public void FindPath()
        {
            path?.ForEach(block => block.Lit = false);

            var up = Direction.Up.RotateBy((int)-transform.rotation.eulerAngles.z);
            Debug.Log(up);
            var upBlock = GetBlock(up);
            path = FindPath(up.Invert(), upBlock, new List<Block> { this, upBlock });
            path?.ForEach(block => block.Lit = true);

            Debug.Log(path == null ? "No path found" : string.Join(", ", path));

            //return path != null;
        }

        private static List<Block> FindPath(Direction dir, Block block, List<Block> alreadyVisited)
        {
            if (block == null)
                return null;

            //Falls der Block die Erde ist
            if (block is Ground)
                return alreadyVisited; // Gib den weg zurück

            Debug.Log(string.Join(", ", alreadyVisited));

            var possibleDirs = block.GetPossiblePaths(dir).ToArray(); //Sonst suche nach anderen Wegen
            var possibleBlocks = possibleDirs.Select(block.GetBlock).Except(alreadyVisited).ToArray(); //Ohne bereits besuchte Blöcke

            var possiblePaths = possibleBlocks.Select(possibleBlock =>
                FindPath(
                    possibleDirs.First(direction => block.GetBlock(direction) == possibleBlock).Invert(),
                    possibleBlock,
                    new List<Block>(alreadyVisited) { possibleBlock }
                )).ToList();

            Debug.Log(string.Join(", ", possibleDirs.ToList()));

            return possiblePaths.Any() ? possiblePaths.Aggregate((list1, list2) => list1?.Count < list2?.Count ? list1 : list2) : null;
        }
    }
}

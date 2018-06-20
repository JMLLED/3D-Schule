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

            path = FindPath(up.Invert(), upBlock, new List<Block> { this, upBlock })?.FirstOrDefault();
            path?.ForEach(block => block.Lit = true);

            return path != null;
        }

        private static IEnumerable<List<Block>> FindPath(Direction dir, Block block, List<Block> alreadyVisited)
        {
            if (block == null)
                return Enumerable.Empty<List<Block>>();

            //Falls der Block die Erde ist
            if (block is Ground && block.GetPossiblePaths(dir).Contains(dir))
                return new[] { alreadyVisited }; // Gib den weg zurück

            var possibleDirs = block.GetPossiblePaths(dir); //Sonst suche nach anderen Wegen
            var possibleBlocks = possibleDirs.Where(possibleDir => block.GetBlock(possibleDir) != null).ToDictionary(block.GetBlock, direction => direction); //Und überprüfe ob diese Wege funktionieren und speichere die Blöcke mit ihrer Richtung ab
            alreadyVisited.ForEach(visitedBlock => possibleBlocks.Remove(visitedBlock)); //gehe nicht über bereits begange Blöcke um einen "Kreislauf" zu verhindern

            var possiblePaths = possibleBlocks.Keys.SelectMany(possibleBlock => //Versuche für jeden Block einen weiteren Weg zu finden
            {
                var newAlreadyVisited = new List<Block>(alreadyVisited) {possibleBlock}; //Erstelle eine neue Liste mit allen bereits besuchten Blöcken
                return FindPath(possibleBlocks[possibleBlock].Invert(), possibleBlock, newAlreadyVisited); //Und versuche dann einen weiteren Weg
            });

            return possiblePaths.OrderBy(list => list.Count);
        }
    }
}

using UnityEngine;

namespace Project.Games.Circuit
{
    public class Playfield : MonoBehaviour
    {
        private const int block_size = 50;

        public TextAsset[] LevelFiles;

        public Block[] BlockTypes;

        private GameObject playfieldContainer;
        private Block[,] blocks;

        public void Start ()
        {
            Load(LevelFiles[0]);
        }

        private void Load(TextAsset levelFile)
        {
            if(playfieldContainer != null)
                Destroy(playfieldContainer);

            var rows = levelFile.text.Split('\n');
            int width = rows[0].Split(',').Length, height = rows.Length;

            playfieldContainer = Instantiate(new GameObject("Playfield", typeof(RectTransform)), transform);
            playfieldContainer.GetComponent<RectTransform>().localPosition = new Vector3(-width, -height) / 2 * block_size;

            blocks = new Block[width,height];

            for (var i = 0; i < height; i++)
            {
                var row = rows[i].Split(',');
                for (var j = 0; j < width; j++)
                {
                    blocks[j, i] = Instantiate(BlockTypes[int.Parse(row[j])], playfieldContainer.transform);
                    blocks[j, i].GetComponent<RectTransform>().localPosition = new Vector2(j, i) * block_size;
                    blocks[j, i].GetComponent<RectTransform>().Rotate(Vector3.back, 90 * Random.Range(0,3));
                }
            }

            for (var i = 0; i < blocks.GetLength(0); i++)
            for (var j = 0; j < blocks.GetLength(1); j++)
            {
                var block = blocks[i, j];
                if (i > 0)
                    block.Left  = blocks[i - 1, j];
                if (i < blocks.GetLength(0) - 1)
                    block.Right = blocks[i + 1, j];
                if (j > 0)
                    block.Up    = blocks[i, j - 1];
                if (j > blocks.GetLength(1) - 1)
                    block.Down  = blocks[i, j + 1];
            }
        }
    }
}

using Project.Games.Circuit.Blocks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Project.Games.Circuit
{
    public class Playfield : MonoBehaviour
    {
        private const int block_size = 50;

        public TextAsset[] LevelFiles;
        private int currentLevel;
        public Button Button;

        public Block[] BlockTypes;

        private GameObject playfieldContainer;
        private Block[,] blocks;
        private Power powerBlock;

        public void Start ()
        {
            Load(LevelFiles[currentLevel]);
            Button.onClick.AddListener(NextLevel);
        }

        private void Load(TextAsset levelFile)
        {
            if(playfieldContainer != null)
                Destroy(playfieldContainer);

            Button.gameObject.SetActive(false);

            var rows = levelFile.text.Split('\n');
            int width = rows[0].Split(',').Length, height = rows.Length;

            playfieldContainer = Instantiate(new GameObject(string.Empty, typeof(RectTransform)), transform);
            playfieldContainer.GetComponent<RectTransform>().localPosition = new Vector3(-width, height) / 2 * block_size;
            playfieldContainer.name = "Playfield";

            blocks = new Block[width,height];

            for (var rowIndex = 0; rowIndex < height; rowIndex++)
            {
                var row = rows[rowIndex].Split(',');
                for (var j = 0; j < width; j++)
                {
                    var block = Instantiate(BlockTypes[int.Parse(row[j])], playfieldContainer.transform);
                    block.GetComponent<RectTransform>().localPosition = new Vector2(j, -rowIndex) * block_size;
                    block.GetComponent<RectTransform>().Rotate(Vector3.back, 90 * Random.Range(0,3));
                    block.name = "Block " + j + "-" + rowIndex;
                    block.OnRotation = OnBlockRotation;
                    blocks[j, rowIndex] = block;

                    var power = block as Power;
                    if (power != null)
                        powerBlock = power;
                }
            }

            for (var i = 0; i < blocks.GetLength(0); i++)
            for (var j = 0; j < blocks.GetLength(1); j++)
            {
                var block = blocks[i, j];
                if (i > 0)
                    block.Left   = blocks[i - 1, j];
                if (i < blocks.GetLength(0) - 1)
                    block.Right  = blocks[i + 1, j];
                if (j > 0)
                    block.Up     = blocks[i, j - 1];
                if (j < blocks.GetLength(1) - 1)
                    block.Down   = blocks[i, j + 1];
            }

            if(powerBlock.FindPath())
                Load(levelFile);
        }

        private void OnBlockRotation()
        {
            Button.gameObject.SetActive(powerBlock.FindPath());
        }

        private void NextLevel()
        {
            if (++currentLevel < LevelFiles.Length)
                Load(LevelFiles[currentLevel]);
            else
            {
                Destroy(playfieldContainer);
                Button.onClick.RemoveListener(NextLevel);
                Button.GetComponentInChildren<Text>().text = "Zurück";
                Button.onClick.AddListener(() => SceneManager.UnloadSceneAsync(gameObject.scene));
            }
        }
    }
}

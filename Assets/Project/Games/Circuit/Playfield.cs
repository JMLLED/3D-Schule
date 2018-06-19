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

        public void Start()
        {
            Load(LevelFiles[currentLevel]);
            Button.onClick.AddListener(NextLevel);
        }

        private void Load(TextAsset levelFile)
        {
            if (playfieldContainer != null) //Ist bereits ein Level geladen wird dieses zerstört
                Destroy(playfieldContainer);

            Button.gameObject.SetActive(false);

            var rows = levelFile.text.Split('\n'); //Die Datei wird in Reihen und Spalten aufgeteilt
            int width = rows[0].Split(',').Length, height = rows.Length;

            playfieldContainer = Instantiate(new GameObject(string.Empty, typeof(RectTransform)), transform); //Das Spielfeld wird erstellt, in das alle Blöcke plaziert werden
            playfieldContainer.GetComponent<RectTransform>().localPosition =
                new Vector3(-width, height) / 2 * block_size;
            playfieldContainer.name = "Playfield";

            blocks = new Block[width, height];

            for (var rowIndex = 0; rowIndex < height; rowIndex++)
            {
                var row = rows[rowIndex].Split(',');
                for (var j = 0; j < width; j++) //Für jeden Block wird ein GameObject erstellt
                {
                    var block = Instantiate(BlockTypes[int.Parse(row[j])], playfieldContainer.transform);
                    block.GetComponent<RectTransform>().localPosition = new Vector2(j, -rowIndex) * block_size; //Richtige position
                    block.GetComponent<RectTransform>().Rotate(Vector3.back, 90 * Random.Range(0, 3)); //Mit einer zufälligen Rotation
                    block.name = "Block " + j + "-" + rowIndex;
                    block.OnRotation = OnBlockRotation;
                    blocks[j, rowIndex] = block;

                    var power = block as Power; //Falls der erstellte Block ein Powerblock ist merken wir ihn uns um später den Weg zu finden
                    if (power != null)
                        powerBlock = power;
                }
            }

            for (var i = 0; i < blocks.GetLength(0); i++) //Jeder der erstellten Böcke bekommt gesagt welcher Block unter/ober/rechts/links von ihnen ist
            for (var j = 0; j < blocks.GetLength(1); j++)
            {
                var block = blocks[i, j];
                if (i > 0)
                    block.Left = blocks[i - 1, j];
                if (i < blocks.GetLength(0) - 1)
                    block.Right = blocks[i + 1, j];
                if (j > 0)
                    block.Up = blocks[i, j - 1];
                if (j < blocks.GetLength(1) - 1)
                    block.Down = blocks[i, j + 1];
            }

            if (powerBlock.FindPath()) //Falls das Level funktionierend geladen wurde wird nochmal versucht es zuladen
                Load(levelFile);
        }

        private void OnBlockRotation() //Bei einer Block rotation wird versucht einen Weg zu finden. Wurde einer gefunden wird der "weiter" Knopf aktiviert
        {
            Button.gameObject.SetActive(powerBlock.FindPath());
        }

        private void NextLevel() //Lädt das nächste Level oder ändert den "weiter" Knopf zu einem "zurück" Knopf, der einen zurück zur Schule bringt
        {
            if (++currentLevel < LevelFiles.Length)
                Load(LevelFiles[currentLevel]);
            else
            {
                Destroy(playfieldContainer); //Das Spielfeld wird zerstört
                Button.onClick.RemoveListener(NextLevel);
                Button.GetComponentInChildren<Text>().text = "Zurück"; //Der Button kriegt einen neuen Text
                Button.onClick.AddListener(() => SceneManager.UnloadSceneAsync(gameObject.scene)); //Und seine Funktion wird verändert
            }
        }
    }
}

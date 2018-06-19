using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Project.Games.Indicator
{
    public class Manager : MonoBehaviour
    {
        public Image SpriteObject;
        public Indicator Indicator;
        public PhObject[] PhObjects;
        public Button NextButton;
        public Dropdown PhDropdown;
        public Text ObjectText;
        public Text ObjectAcidText;
        private List<PhObject> possiblePhObjects;
        private PhObject phObject;
        private string objectStartText, objectAcidStartText;

        public static readonly PhValue[] IndicatorValues = //Alle Indikator, deren Farbe und PH-Wert
        {
            new PhValue
            {
                PhStart = double.MinValue,
                PhEnd = 3,
                Name = "stark sauer",
                Color = Color.red
            },
            new PhValue
            {
                PhStart = 3,
                PhEnd = 6,
                Name = "leicht sauer",
                Color = Color.yellow,
            },
            new PhValue
            {
                PhStart = 6,
                PhEnd = 8,
                Name = "neutral",
                Color = Color.green,
            },
            new PhValue
            {
                PhStart = 8,
                PhEnd = 11,
                Name = "leicht basisch",
                Color = Color.blue,
            },
            new PhValue
            {
                PhStart = 11,
                PhEnd = double.MaxValue,
                Name = "stark basisch",
                Color = Color.magenta,
            },
        };

        public static PhValue GetPhValue(double ph) //Gibt das Object zu einem PH-Wert zurück
        {
            return IndicatorValues.First(value => value.InsideRange(ph));
        }

        public void Start()
        {
            possiblePhObjects = new List<PhObject>(PhObjects); //Am Anfang wird eine Liste für alle Objekte erstellt
            objectStartText = ObjectText.text;    //Der Anfangstext wird gespeichert und die Werte einfach drangehängt
            objectAcidStartText = ObjectAcidText.text;

            NextObject(); //Zuguterletzt wird das erste Objekt erstellt
        }

        private void NextObject()
        {
            phObject = possiblePhObjects[Random.Range(0, possiblePhObjects.Count)]; //Wähle ein zufälliges Objekt aus und entferne es von den möglichen Objekten
            possiblePhObjects.Remove(phObject);

            SpriteObject.sprite = phObject.Sprite;
            var size = new Vector2(phObject.Sprite.texture.width, phObject.Sprite.texture.height); //Ändere das Bild und die Größe des GameObjects
            SpriteObject.GetComponent<RectTransform>().sizeDelta = size;
            SpriteObject.GetComponent<BoxCollider2D>().size = size;

            Indicator.PhValue = phObject.PhValue;
            ObjectText.text = objectStartText + phObject.Name; //Ändere den Text
            ObjectAcidText.text = objectAcidStartText + phObject.AcidName;
            Indicator.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public void OnNextButtonClick() //Wird ausgeführt wenn mann den Knopf drückt
        {
            if (IndicatorValues[PhDropdown.value] == GetPhValue(phObject.PhValue)) //Falls das richtige ausgewählt wurde
            {
                StartCoroutine(FlashButton(Color.green)); //Der Knopf blinkt grün auf
                if (possiblePhObjects.Count == 0)                    //Jenachdem ob es noch Objekte gibt wird
                    SceneManager.UnloadSceneAsync(gameObject.scene); //Die Szene entladen
                else                                                 //Sonst
                    NextObject();                                    //Wird das nächste Objekt gezeigt
            }
            else
                StartCoroutine(FlashButton(Color.red)); //Der Knopf blinkt rot auf
        }

        private IEnumerator FlashButton(Color color)
        {
            float startTime = Time.time; //Die Anfangszeit
            while (startTime + 1 > Time.time) //Bis eine Sekunde
            {
                NextButton.GetComponent<Image>().color = Color.Lerp(color, Color.white, Time.time - startTime); //Interpoliere zwischen Weiß un der Farbe entsprechend der Zeit
                yield return null;
            }
        }
    }

    public class PhValue //Speichert alles Wichtige über eine Indikatorfarbe
    {
        public double PhStart, PhEnd;
        public string Name;
        public Color Color;

        public bool InsideRange(double value)
        {
            return PhStart <= value && value < PhEnd;
        }
    }
}

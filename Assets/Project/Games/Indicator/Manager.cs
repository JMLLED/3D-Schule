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

        public static readonly PhValue[] IndicatorValues =
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

        public static PhValue GetPhValue(double ph)
        {
            return IndicatorValues.First(value => value.InsideRange(ph));
        }

        public void Start()
        {
            possiblePhObjects = new List<PhObject>(PhObjects);
            objectStartText = ObjectText.text;
            objectAcidStartText = ObjectAcidText.text;

            NextObject();
        }

        private void NextObject()
        {
            phObject = possiblePhObjects[Random.Range(0, possiblePhObjects.Count)];
            possiblePhObjects.Remove(phObject);

            SpriteObject.sprite = phObject.Sprite;
            var size = new Vector2(phObject.Sprite.texture.width, phObject.Sprite.texture.height);
            SpriteObject.GetComponent<RectTransform>().sizeDelta = size;
            SpriteObject.GetComponent<BoxCollider2D>().size = size;

            Indicator.PhValue = phObject.PhValue;
            ObjectText.text = objectStartText + phObject.Name;
            ObjectAcidText.text = objectAcidStartText + phObject.AcidName;
            Indicator.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public void OnNextButtonClick()
        {
            if (IndicatorValues[PhDropdown.value] == GetPhValue(phObject.PhValue))
            {
                StartCoroutine(FlashButton(Color.green));
                if (possiblePhObjects.Count == 0)
                    SceneManager.UnloadSceneAsync(gameObject.scene);
                else
                    NextObject();
            }
            else
                StartCoroutine(FlashButton(Color.red));
        }

        private IEnumerator FlashButton(Color color)
        {
            float startTime = Time.time;
            while (startTime + 1 > Time.time)
            {
                NextButton.GetComponent<Image>().color = Color.Lerp(color, Color.white, Time.time - startTime);
                yield return null;
            }
        }
    }

    public class PhValue
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

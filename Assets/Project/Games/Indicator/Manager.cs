using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Games.Indicator
{
    public class Manager : MonoBehaviour
    {
        public Image SpriteObject;
        public Indicator Indicator;
        public PhObject[] PhObjects;
        private List<PhObject> possiblePhObjects;

        void Start()
        {
            possiblePhObjects = new List<PhObject>(PhObjects);

            NextObject();
        }

        private void NextObject()
        {
            var nextPhObject = possiblePhObjects[Random.Range(0, possiblePhObjects.Count)];
            possiblePhObjects.Remove(nextPhObject);

            SpriteObject.sprite = nextPhObject.Sprite;
            SpriteObject.GetComponent<RectTransform>().sizeDelta = new Vector2(nextPhObject.Sprite.texture.width,
                nextPhObject.Sprite.texture.height);

            Indicator.PhValue = nextPhObject.PhValue;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace Project.Games.Indicator
{
    public class Indicator : MonoBehaviour
    {
        public void Update ()
        {
            double ph = GetPhValue();
            Color color;
            if(ph < 3) color = Color.red;
            else if(ph < 6) color = Color.yellow;
            else if(ph < 8) color = Color.green;
            else if(ph < 11) color = Color.blue;
            else color = Color.magenta;

            transform.GetComponent<Image>().color = color;
        }

        private double GetPhValue() => RectOverlaps(GetComponent<RectTransform>(), PhObjectSprite) ? PhValue : 7;

        public RectTransform PhObjectSprite;

        internal double PhValue;

        private Vector2? lastPosition;

        public void OnMouseDrag()
        {
            Vector2 mousePosition = Input.mousePosition;
            var delta = mousePosition - (lastPosition ?? mousePosition);

            // ReSharper disable once RedundantCast
            transform.position += (Vector3)delta;

            lastPosition = mousePosition;
        }

        private static bool RectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
        {
            Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
            Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

            return rect1.Overlaps(rect2);
        }
    }
}

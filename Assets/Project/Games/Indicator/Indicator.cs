using UnityEngine;
using UnityEngine.UI;

namespace Project.Games.Indicator
{
    public class Indicator : MonoBehaviour
    {
        public Image ColorImage;

        public void Update()
        {
            double? ph = GetPhValue();
            ColorImage.color = ph == null ? Color.clear : Manager.GetPhValue(ph.Value).Color;
        }

        private double? GetPhValue() => GetComponent<Collider2D>().Distance(PhObjectSprite).isOverlapped ? (double?) PhValue : null;

        public Collider2D PhObjectSprite;

        internal double PhValue;

        private Vector2? lastPosition;

        public void OnMouseDrag()
        {
            Vector2 mousePosition = Input.mousePosition;
            var delta = mousePosition - (lastPosition ?? mousePosition);

            GetComponent<RectTransform>().anchoredPosition += delta;

            lastPosition = mousePosition;
        }

        public void OnMouseDragEnd()
        {
            lastPosition = null;
        }
    }
}

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

        private double? GetPhValue() => GetComponent<Collider2D>().Distance(PhObjectSprite).isOverlapped ? (double?) PhValue : null; //Überprüft ob der indikator über dem Objekt ist und falls ja gibt er den richtigen PH-Wert zurück

        public Collider2D PhObjectSprite;

        internal double PhValue;

        private Vector2? lastPosition;

        public void OnMouseDrag()
        {
            Vector2 mousePosition = Input.mousePosition;
            var delta = mousePosition - (lastPosition ?? mousePosition); //Rechne aus wie die Mausposition sich verändert hat und bewegen den Indikator entsprechend

            GetComponent<RectTransform>().anchoredPosition += delta;

            lastPosition = mousePosition;
        }

        public void OnMouseDragEnd()
        {
            lastPosition = null;
        }
    }
}

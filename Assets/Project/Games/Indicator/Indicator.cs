using UnityEngine;
using UnityEngine.UI;

namespace Project.Games.Indicator
{
    public class Indicator : MonoBehaviour
    {
        public Image ColorImage;

        public void Update ()
        {
            double? ph = GetPhValue();
            Color color;
            if(ph < 3) color = Color.red;
            else if(ph < 6) color = Color.yellow;
            else if(ph < 8) color = Color.green;
            else if(ph < 11) color = Color.blue;
            else if(ph >= 11) color = Color.magenta;
            else color = new Color(0,0,0,0);

            ColorImage.color = color;
        }

        private double? GetPhValue() => GetComponent<Collider2D>().Distance(PhObjectSprite).isOverlapped ? (double?) PhValue : null;

        public Collider2D PhObjectSprite;

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
    }
}

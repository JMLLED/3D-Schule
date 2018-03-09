using UnityEngine;
using UnityEngine.UI;

namespace Project.Games.Indicator
{
    public class Indicator : MonoBehaviour
    {
        public void Update ()
        {
            double ph = GetPHValue();
            Color color;
            if(ph < 3) color = Color.red;
            else if(ph < 6) color = Color.yellow;
            else if(ph < 8) color = Color.green;
            else if(ph < 11) color = Color.blue;
            else color = Color.magenta;

            transform.GetComponent<Image>().color = color;
        }

        private double GetPHValue() => Time.time % 14;

        public void OnMouseDrag()
        {

            Vector2 delta = new Vector2(Input.GetAxisRaw("Mouse X"),Input.GetAxisRaw("Mouse Y"))*15f;

            Vector2 position = transform.position;
            position.x += delta.x;
            position.y += delta.y;
            transform.position = position;

            Debug.Log(position);
        }
    }
}

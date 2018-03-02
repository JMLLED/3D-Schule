using UnityEngine;

namespace Project.Games.Indicator
{
    public class Indicator : MonoBehaviour
    {
        public void Update ()
        {
            double ph = GetPHValue();
            Material material = transform.GetComponent();
            if(ph < 3) material.color = Color.red;
            else if(ph < 6) material.color = Color.yellow;
            else if(ph < 8) material.color = Color.green;
            else if(ph < 11) material.color = Color.blue;
            else material.color = Color.magenta;
        }

        private double GetPHValue() => 7;
    }
}

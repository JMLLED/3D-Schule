using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Games.Indicator
{
    public class DropdownManager : MonoBehaviour
    {
        private void Start()
        {
            var dropdown = GetComponent<Dropdown>();

            dropdown.AddOptions(Manager.IndicatorValues.Select(value => //Für Jede Farbe wird ein Dropdown-Item erstellt
            {
                var texture = new Texture2D(1, 1);
                texture.SetPixel(0, 0, value.Color);
                texture.Apply();
                return new Dropdown.OptionData(value.Name,
                    Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(texture.width, texture.height)),
                        Vector2.zero));
            }).ToList());
            dropdown.RefreshShownValue(); //Und erneuer die GameObjects des Dropdowns
        }
    }
}

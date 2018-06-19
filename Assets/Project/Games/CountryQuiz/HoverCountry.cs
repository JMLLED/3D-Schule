using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Games.CountryQuiz
{
    public class HoverCountry : MonoBehaviour
    {
        private Sprite greyTex, tex;
        private Image image;
        private bool grey = true;

        internal event Action OnClick;

        private void Start()
        {
            image = GetComponent<Image>();

            tex = image.sprite;
            greyTex = CreateGreyTexture(tex);

            image.sprite = greyTex;
        }

        private static Sprite CreateGreyTexture(Sprite tex)
        {
            var greyTex = new Texture2D(tex.texture.width, tex.texture.height, TextureFormat.ARGB32, false); //Erstellt eine neue Textur die die graue Textur enthält

            greyTex.SetPixels(tex.texture.GetPixels()
                .Select(color => new Color(color.grayscale - 0.1f, color.grayscale - 0.1f, color.grayscale - 0.1f, color.a)).ToArray()); //Jeder der Pixel in der neuen Textur ist der glaiche Pixel wir in der alten Textur bloß grau
            greyTex.alphaIsTransparency = true;
            greyTex.Apply();    //Die Textur wird gespeichert

            return Sprite.Create(greyTex, tex.rect, tex.pivot); //Und es wird ein Sprite daraus gemacht
        }

        private void Update()
        {
            Vector2 point = Input.mousePosition;
            RectTransform trans = GetComponent<RectTransform>();
            point = trans.InverseTransformPoint(point); //Mausposition auf dem Bild
            Vector2 texturePoint = Rect.NormalizedToPoint(tex.rect, Rect.PointToNormalized(trans.rect, point)); //Pixel auf dem die Maus ist

            if ((grey || Input.GetMouseButtonDown(0)) && trans.rect.Contains(point) && tex.texture.GetPixel((int)texturePoint.x, (int)texturePoint.y).a > 0.1)    //Ist die Maustaste gedrückt oder das Bild grau und ist die Maus auf einem nicht transparenten Pixel
            {
                grey = false;
                image.sprite = tex;    //Das Bild wird farbig

                if (Input.GetMouseButtonDown(0)) //Falls geklickt wurde
                {
                    OnClick?.Invoke(); //Wird ein Event aufgeführt
                }
            }
            else if (!grey && (!trans.rect.Contains(point) || tex.texture.GetPixel((int)texturePoint.x, (int)texturePoint.y).a < 0.1)) //Ist das Bild farbig und die Maus außerhalb des Bilds
            {
                grey = true;
                image.sprite = greyTex; //Das Bild wird grau
            }
        }
    }
}

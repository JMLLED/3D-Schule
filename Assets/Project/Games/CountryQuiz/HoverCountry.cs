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
            var greyTex = new Texture2D(tex.texture.width, tex.texture.height, TextureFormat.ARGB32, false);

            greyTex.SetPixels(tex.texture.GetPixels()
                .Select(color => new Color(color.grayscale - 0.1f, color.grayscale - 0.1f, color.grayscale - 0.1f, color.a)).ToArray());
            greyTex.alphaIsTransparency = true;
            greyTex.Apply();

            return Sprite.Create(greyTex, tex.rect, tex.pivot);
        }

        private void Update()
        {
            Vector2 point = Input.mousePosition;
            RectTransform trans = GetComponent<RectTransform>();
            point = trans.InverseTransformPoint(point);
            Vector2 texturePoint = Rect.NormalizedToPoint(tex.rect, Rect.PointToNormalized(trans.rect, point));

            if ((grey || Input.GetMouseButtonDown(0)) && trans.rect.Contains(point) && tex.texture.GetPixel((int)texturePoint.x, (int)texturePoint.y).a > 0.1)
            {
                grey = false;
                image.sprite = tex;

                if (Input.GetMouseButtonDown(0))
                {
                    OnClick?.Invoke();
                }
            }
            else if (!grey && (!trans.rect.Contains(point) || tex.texture.GetPixel((int)texturePoint.x, (int)texturePoint.y).a < 0.1))
            {
                grey = true;
                image.sprite = greyTex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Games.Circuit
{
    public abstract class Block : MonoBehaviour
    {
        public Block Up, Down, Left, Right;

        public Sprite UnlitSprite, LitSprite;

        public Action OnRotation;

        protected abstract IEnumerable<Direction> GetConnectionsFrom(Direction dir);

        public IEnumerable<Direction> GetPossiblePaths(Direction dir)
        {
            var rotation = (int)transform.rotation.eulerAngles.z;
            return GetConnectionsFrom(dir.RotateBy(rotation)).Select(direction => direction.RotateBy(-rotation));
        }

        public bool Lit //Ändert die Textur für den richtigen Weg
        {
            set { gameObject.GetComponent<Image>().sprite = value ? LitSprite : UnlitSprite; }
        }

        public Block GetBlock(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return Up;
                case Direction.Right:
                    return Right;
                case Direction.Down:
                    return Down;
                case Direction.Left:
                    return Left;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }

        public void Rotate() //Rotiert den Block
        {
            transform.Rotate(Vector3.back * 90);
            OnRotation();
        }
    }

    public enum Direction
    {
        Up     = 0,
        Right  = 1,
        Down   = 2,
        Left   = 3
    }

    public static class DirectionExtensions
    {
        public static Direction Invert(this Direction dir) //Invertiert die Richtung
        {
            return (Direction)(((int)dir + 2) % 4);
        }

        public static Direction RotateBy(this Direction dir, int rotation) //Gibt die rotierte Richtung zurück
        {
            return (Direction)(((int)dir + 4 + rotation / 90) % 4);
        }
    }
}

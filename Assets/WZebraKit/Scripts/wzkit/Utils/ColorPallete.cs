using UnityEngine;

using Sirenix.OdinInspector;

using wzebra.kit.data;

namespace wzebra.kit.utils
{
    public class ColorPallete : MonoBehaviour
    {
        private static ColorPallete _instance;

        public static ColorPallete Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new System.NullReferenceException("Can't find ColorPallete");
                }
                else
                {
                    return _instance;
                }
            }
        }

        [SerializeField] private ColorReference[] _colors;

        private void Awake()
        {
            _instance = this;
        }

        public Color GetColor(GameColor color)
        {
            for (int i = 0; i < _colors.Length; i++)
            {
                if(_colors[i].GameColor == color)
                {
                    return _colors[i].Color;
                }
            }

            throw new System.Exception($"Can't find color {color} in pallete.");
        }

        [System.Serializable]
        public struct ColorReference
        {
            [HorizontalGroup("Group"), LabelWidth(100)]
            public GameColor GameColor;
            [HorizontalGroup("Group")]
            public Color Color;
        }
    }
}
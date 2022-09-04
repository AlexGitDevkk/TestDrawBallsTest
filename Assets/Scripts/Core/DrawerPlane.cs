using UnityEngine;

using Sirenix.OdinInspector;

using wzebra.kit.data;
using wzebra.kit.utils;

namespace wzebra.drawballs.core
{
    public class DrawerPlane : MonoBehaviour
    {
        [SerializeField] private GameColor _color;

        public bool HasColor(GameColor color) => _color == color || _color == GameColor.Any;

        private void Start()
        {
            GetComponent<Renderer>().material.SetColor("_BaseColor", ColorPallete.Instance.GetColor(_color));
        }
    }
}
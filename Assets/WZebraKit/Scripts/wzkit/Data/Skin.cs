using UnityEngine;

namespace wzebra.kit.data
{
    [System.Serializable]
    public class Skin
    {
        [SerializeField] private GameColor _color;
        [SerializeField] private Material _material;

        public GameColor Color => _color;

        public Material Material => _material;
    }
}
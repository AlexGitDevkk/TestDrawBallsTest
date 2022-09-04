using UnityEngine;

namespace wzebra.kit.core
{
    public class SpriteOnScreenScaler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;

        [Range(0, 1)]
        [SerializeField] private float _maxWidth = 1;
        [Range(0, 1)]
        [SerializeField] private float _maxHeight = 1;

        private void Awake()
        {
            float height = Camera.main.orthographicSize * 2;
            float width = height * Screen.width / Screen.height;

            Sprite s = _sprite.sprite;
            float unitWidth = (s.textureRect.width / s.pixelsPerUnit) * _sprite.transform.localScale.x;
            float unitHeight = (s.textureRect.height / s.pixelsPerUnit) * _sprite.transform.localScale.y;

            float scale = (width / unitWidth);

            if (unitHeight * scale > height * _maxHeight)
            {
                scale = (height * _maxHeight) / unitHeight;
            }

            if (unitWidth * scale > width * _maxWidth)
            {
                scale = (width * _maxWidth) / unitWidth;
            }

            transform.localScale = new Vector3(scale, scale);
        }
    }
}
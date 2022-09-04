using System.Collections.Generic;

using UnityEngine;

using wzebra.kit.data;
using wzebra.kit.utils;
using wzebra.drawballs.core;
using wzebra.drawballs.data;

namespace wzebra.drawballs.ui
{
    public class ColorSelector : MonoBehaviour
    {
        [SerializeField] private LevelSpawner _spawner;

        [SerializeField] private GameObject _selectorElement;

        [SerializeField] private BallsDrawer _drawer;

        private ColorSelectorElement _currentSelect;

        private Dictionary<GameColor, ColorSelectorElement> _elements;

        private void Start()
        {
            _spawner.OnStartLevel += OnStartLevel;

            _drawer.OnSpawnBall += OnSpawnBall;
        }

        private void OnSpawnBall(GameColor color)
        {
            _elements[color].SetCount(_drawer.GetRestCountColor(color));
        }

        private void OnStartLevel()
        {
            transform.DestroyAllChilds();

            _currentSelect = null;

            ColorCount[] colors = _spawner.GetCurrentLevel().ColorsData;

            _elements = new Dictionary<GameColor, ColorSelectorElement>();

            for (int i = 0; i < _spawner.GetCurrentLevel().ColorsData.Length; i++)
            {
                ColorCount data = colors[i];

                GameObject go = Instantiate(_selectorElement, transform);

                ColorSelectorElement element = go.GetComponent<ColorSelectorElement>();

                element.SetColor(ColorPallete.Instance.GetColor(data.Color));
                element.SetCount(data.Count);
                element.OnSelect += () => 
                {
                    if (_currentSelect != null)
                    {
                        _currentSelect.Select(false);
                    }

                    _currentSelect = element;
                    _currentSelect.Select(true);
                    _drawer.SetColor(data.Color); 
                };

                _elements.Add(data.Color, element);

                if(_currentSelect == null)
                {
                    element.OnSelectInvoke();
                }
            }
        }
    }
}
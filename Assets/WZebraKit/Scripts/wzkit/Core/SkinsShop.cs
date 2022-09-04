using UnityEngine;

using Sirenix.OdinInspector;

using wzebra.kit.data;

namespace wzebra.kit.core
{
    public class SkinsShop : Shop<Skin[]>
    {
        protected override void AfterAwake()
        {
            
        }

        public Skin[] GetSkins() => GetSelected().Element;
    }
}
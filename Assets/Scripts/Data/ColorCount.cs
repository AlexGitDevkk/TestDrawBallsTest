using Sirenix.OdinInspector;

using wzebra.kit.data;

namespace wzebra.drawballs.data
{
    [System.Serializable]
    public struct ColorCount
    {
        [HorizontalGroup("Group"), LabelWidth(100)]
        public GameColor Color;
        [HorizontalGroup("Group")]
        public int Count;
    }
}

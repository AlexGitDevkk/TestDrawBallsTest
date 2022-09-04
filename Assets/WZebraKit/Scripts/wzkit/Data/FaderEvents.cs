using UltEvents;

namespace wzebra.kit.data
{
    [System.Serializable]
    public class FaderEvents
    {
        public UltEvent OnStartInFading;
        public UltEvent OnStartOutFading;

        public UltEvent OnEndInFading;
        public UltEvent OnEndOutFading;
    }
}
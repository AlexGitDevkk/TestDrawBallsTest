using UnityEngine;

namespace wzebra.kit.utils
{
    public class CoinsSaver : MonoBehaviour
    {
        private const string _key = "Coins";

        public static int GetCount(int alternative)
        {
            return PlayerPrefs.GetInt(_key, alternative);
        }

        public static void SetCount(int count)
        {
            PlayerPrefs.SetInt(_key, count);
            PlayerPrefs.Save();
        }
    }
}
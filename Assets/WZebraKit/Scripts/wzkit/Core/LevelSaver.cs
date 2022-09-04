using UnityEngine;

namespace wzebra.kit.core
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/LevelSaver")]
    public class LevelSaver
    {
        private const string ACTUAL_LEVEL = "ActualLevel";
        private const string SHOWING_LEVEL = "ShowLevel";

        public static void SetActualLevel(int level)
        {
            SetInt(ACTUAL_LEVEL, level);
        }

        public static void SetShowingLevel(int level)
        {
            SetInt(SHOWING_LEVEL, level);
        }

        private static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public static int GetActualLevel() => GetInt(ACTUAL_LEVEL);

        public static int GetShowingLevel() => GetInt(SHOWING_LEVEL);

        private static int GetInt(string key) => PlayerPrefs.GetInt(key, 0);
    }
}
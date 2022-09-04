using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

using wzebra.kit.core;

namespace wzebra.kit.utils
{
    public class SetLevelGUI : EditorWindow
    {
        private int _level;

        [MenuItem("Tools/White Zebra/Set Level")]
        static void Init()
        {
            EditorWindow window = CreateInstance<SetLevelGUI>();

            window.titleContent = new GUIContent("Читерское окошко");
            window.ShowModal();
        }

        private void CreateGUI()
        {
            _level = LevelSaver.GetActualLevel();
        }

        void OnGUI()
        {
            GUILayout.Space(10);
            _level = EditorGUILayout.IntField("Текущий уровень: ", _level);
            GUILayout.Space(10);
            if (GUILayout.Button("Set"))
            {
                Set();
            }

            if (GUILayout.Button("Set & Play"))
            {
                if (Set())
                {
                    EditorApplication.isPlaying = true;
                }
            }

            if (GUILayout.Button("Reset & Play"))
            {
                _level = 0;
                Set();

                EditorApplication.isPlaying = true;
            }

            if (GUILayout.Button("Reset Total & Play"))
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();

                EditorApplication.isPlaying = true;

                Close();
            }

            GUILayout.Space(10);

            EditorGUILayout.LabelField("P.S.: Помним, что нумерация");
            EditorGUILayout.LabelField("начинается с 0!");
        }

        private bool Set()
        {
            if (_level < 0)
            {
                return false;
            }
            else
            {
                LevelSaver.SetActualLevel(_level);
                LevelSaver.SetShowingLevel(_level);

                Close();
                return true;
            }
        }
    }
}
#endif
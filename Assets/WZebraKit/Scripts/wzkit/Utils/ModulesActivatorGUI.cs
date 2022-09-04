using System.Linq;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace wzebra.kit.utils
{
    public class ModulesActivatorGUI : EditorWindow
    {
        private Dictionary<string, bool> _activeModules = new Dictionary<string, bool>();

        [MenuItem("Tools/White Zebra/Modules settings")]
        static void Init()
        {
            EditorWindow window = CreateInstance<ModulesActivatorGUI>();

            window.titleContent = new GUIContent("Modules settings");
            window.ShowModal();
        }

        private void CreateGUI() 
        {
            for (int i = 0; i < ModulesType.TYPES.Length; i++)
            {
                _activeModules.Add(ModulesType.TYPES[i], IsAnalyticEnable(ModulesType.TYPES[i]));
            }
        }

        void OnGUI()
        {
            GUILayout.Space(30);
            for (int i = 0; i < _activeModules.Count; i++)
            {
                string name = _activeModules.ElementAt(i).Key;

                bool enable = GUILayout.Toggle(_activeModules.ElementAt(i).Value, name);

                _activeModules[name] = enable;
            }

            if (GUILayout.Button("Apply"))
            {
                ApplyChanges();
                Debug.Log("Modules setted! Please, wait for compilation.");
                Close();
            }
        }

        private bool IsAnalyticEnable(string name) => GetAllDefineSymbols().Contains(name);

        private void ApplyChanges()
        {
            List<string> defines = new List<string>(GetAllDefineSymbols());

            foreach(KeyValuePair<string, bool> pair in _activeModules)
            {
                if(defines.Contains(pair.Key) && pair.Value == false)
                {
                    defines.Remove(pair.Key);
                }else if(defines.Contains(pair.Key) == false && pair.Value)
                {
                    defines.Add(pair.Key);
                }
            }

            PlayerSettings.SetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.Android, defines.ToArray());
        }

        private string[] GetAllDefineSymbols()
        {
            string[] defines = null;

            PlayerSettings.GetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.Android, out defines);

            return defines;
        }
    }
}
#endif
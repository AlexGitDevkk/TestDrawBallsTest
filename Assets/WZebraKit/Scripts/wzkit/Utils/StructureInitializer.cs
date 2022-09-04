using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

namespace wzebra.kit.utils
{
    public class StructureInitializer
    {
        [MenuItem("Tools/White Zebra/Init project structure/2D")]
        public static void Init2D()
        {
            InitGeneral();

            AddFolder("Graphics", "Sprite");

            Debug.Log("2D structure initialized!");
        }

        [MenuItem("Tools/White Zebra/Init project structure/3D")]
        public static void Init3D()
        {
            InitGeneral();

            AddFolder("", "Models");

            Debug.Log("3D structure initialized!");
        }

        private static void InitGeneral()
        {
            AddFolder("", "Graphics");

            AddFolder("Graphics", "UI");
            AddFolder("Graphics", "Textures");

            AddFolder("", "Shaders");
            AddFolder("", "Materials");
            AddFolder("", "Prefabs");
            AddFolder("", "Sounds");

            AddFolder("", "Animations");
            AddFolder("Animations", "Animators");
            AddFolder("Animations", "Clips");

            AddFolder("Prefabs", "UI");
            AddFolder("Prefabs", "Gameplay");
            AddFolder("Prefabs", "Levels");
            AddFolder("Prefabs", "General");
        }

        private static void AddFolder(string path, string folderName)
        {
            AssetDatabase.CreateFolder("Assets" + (path.Length > 0 ? "/" : "") + path, folderName);
        }
    }
}

#endif
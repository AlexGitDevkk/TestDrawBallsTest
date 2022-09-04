using Sirenix.OdinInspector;
using UnityEngine;

namespace wzebra.kit.data
{
    [System.Serializable]
    public class LevelGO
    {
        [PreviewField(Alignment = ObjectFieldAlignment.Center, Height = 90), HideLabel(), Title("@Level?.name ?? \"No Level\""), HorizontalGroup("Group")]
        public GameObject Level;

        [Button("Play"), HorizontalGroup("Group"), ShowIf("@Level!=null"), ExecuteInEditMode()]
        public void Play()
        {
            PlayerPrefs.SetInt("StartFrom", Level.transform.GetInstanceID());
            PlayerPrefs.Save();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = true;
#endif
        }
    }
}
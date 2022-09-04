using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

using wzebra.kit.tweener;

namespace wzebra.kit.utils
{
    [CustomEditor(typeof(ObjectAnimation), true)]
    public class TransformAnimationCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Label("Only in play mode!");

            if (GUILayout.Button("Play"))
            {
                ((TransformAnimation)target).Play();
            }

            if (GUILayout.Button("Rewind"))
            {
                ((TransformAnimation)target).Rewind(false);
            }

            if (GUILayout.Button("Rewind with animation"))
            {
                ((TransformAnimation)target).Rewind(true);
            }
        }
    }
}

#endif
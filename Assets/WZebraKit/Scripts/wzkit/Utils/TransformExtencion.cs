using UnityEngine;

namespace wzebra.kit.utils
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/TransformExtencion")]
    public static class TransformExtencion
    {
        public static void DestroyAllChilds(this Transform transform, bool immediate = false)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (immediate) 
                {
                    Object.DestroyImmediate(transform.GetChild(i).gameObject);
                }
                else
                {
                    Object.Destroy(transform.GetChild(i).gameObject);
                }
            }
        }

        public static void SetActiveChilds(this Transform transform, bool active)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(active);
            }
        }
    }
}
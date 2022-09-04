using UnityEngine;

namespace wzebra.kit.core
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/HapticPlayer")]
    public class HapticPlayer : MonoBehaviour
    {
        public void Play(int power)
        {
            switch (power)
            {
                case 0:
                    Taptic.Light();
                    break;
                case 1:
                    Taptic.Medium();
                    break;
                case 2:
                    Taptic.Heavy();
                    break;
                default:
                    throw new System.Exception("Unknown type of taptic power");
            }
        }
    }
}
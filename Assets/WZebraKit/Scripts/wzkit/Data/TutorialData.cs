using UnityEngine;

namespace wzebra.kit.data
{
    [System.Serializable]
    public class TutorialData
    {
        [SerializeField] private GameObject _tutorialObject;
        [SerializeField] private int _targetLevel;
        [SerializeField] private int _customEvent = -1;

        public GameObject TutorialObject => _tutorialObject;

        public int TargetLevel => _targetLevel;

        public int CustomEvent => _customEvent;
    }
}
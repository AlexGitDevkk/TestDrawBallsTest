using System.Linq;
using System.Collections;

#if INAPPREVIEW

using Google.Play.Review;

using UnityEngine;

using wzebra.kit.core;

namespace wzebra.kit.analytics
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/Аналитика.-In-App-Review")]
    public class InAppReview : MonoBehaviour
    {
        [SerializeField] private LevelSpawner _spawner;
        [SerializeField] private LevelCounter _counter;

        [SerializeField] private int _targetLevel;

        private ReviewManager _reviewManager;
        private PlayReviewInfo _playReviewInfo;

#if PLATFORM_ANDROID
        private void Awake()
        {
            _reviewManager = new ReviewManager();

            _spawner.OnStartLevel += OnStartLevel;
        }

        private void OnStartLevel()
        {
            if (_targetLevel == _counter.GetShowingLevel())
            {
                StartCoroutine(LoadReview());
            }
        }

        private IEnumerator LoadReview()
        {
            _spawner.OnEndLevel += OnEndLevel;

            var requestFlowOperation = _reviewManager.RequestReviewFlow();
            yield return requestFlowOperation;

            if (requestFlowOperation.Error != ReviewErrorCode.NoError)
            {
                Debug.LogError(requestFlowOperation.Error.ToString());
                yield break;
            }

            _playReviewInfo = requestFlowOperation.GetResult();
        }

        private void OnEndLevel()
        {
            _spawner.OnEndLevel -= OnEndLevel;

            StartCoroutine(ShowReview());
        }

        private IEnumerator ShowReview()
        {
            var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);

            yield return launchFlowOperation;
            _playReviewInfo = null;

            if (launchFlowOperation.Error != ReviewErrorCode.NoError)
            {
                Debug.LogError(launchFlowOperation.Error.ToString());
                yield break;
            }
        }
    }
#endif
}

#endif
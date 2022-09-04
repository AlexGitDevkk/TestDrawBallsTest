using UnityEngine;

using Sirenix.OdinInspector;

using wzebra.drawballs.data;

namespace wzebra.drawballs.core
{
    public class Level : kit.core.LevelBase
    {
        [SerializeField, TabGroup("Draw Balls")] private ColorCount[] _colors;

        public ColorCount[] ColorsData => _colors;

        private int _bucketCount;

        private void Start()
        {
            Bucket[] buckets = GetComponentsInChildren<Bucket>();

            foreach (var bucket in buckets)
            {
                bucket.OnAction += BucketDecrement;
            }

            _bucketCount = buckets.Length;
        }

        private void BucketDecrement()
        {
            _bucketCount--;

            if(_bucketCount == 0)
            {
                WinInvoke();
            }
        }
    }
}
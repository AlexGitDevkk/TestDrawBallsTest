using System;
using UnityEngine;

using wzebra.kit.core;

namespace wzebra.kit.utils
{
    public class EnergySaver
    {
        private const string _countKey = "EnergyCount", _regenerationTime = "EnergyTime";

        private readonly EnergyCounter _counter;

        public EnergySaver(EnergyCounter counter)
        {
            _counter = counter;
            _counter.OnCounterUpdate += SetCount;
        }

        public int GetCount() => PlayerPrefs.GetInt(_countKey, _counter.GetMaxCount());

        public float GetRegenerationState(int duration)
        {
            DateTime time = DateTime.Parse(PlayerPrefs.GetString(_regenerationTime, DateTime.UtcNow.ToString()));

            TimeSpan span = DateTime.UtcNow - time;

            int seconds = (int)span.TotalSeconds;

            return (float)seconds / duration;
        }

        public void SetCount(int count)
        {
            PlayerPrefs.SetInt(_countKey, count);
            PlayerPrefs.Save();
        }

        public void SetRegenerationTime(DateTime time)
        {
            PlayerPrefs.SetString(_regenerationTime, time.ToString());
            PlayerPrefs.Save();
        }
    }
}
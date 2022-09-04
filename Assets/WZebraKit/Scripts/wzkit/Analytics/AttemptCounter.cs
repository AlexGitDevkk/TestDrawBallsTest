using UnityEngine;

namespace wzebra.kit.analytics
{
    public class AttemptCounter 
    {
        private int _attempt;

        private const string _key = "Attempt";

        private readonly string _id;

        public AttemptCounter(string uniqueID)
        {
            _id = uniqueID;
        }

        public int GetAttempt()
        {
            _attempt = PlayerPrefs.GetInt(_key + _id, 0);
            return _attempt;
        }

        public void IncrementAttempt()
        {
            PlayerPrefs.SetInt(_key + _id, ++_attempt);
            PlayerPrefs.Save();
        }

        public void DeleteAttempt()
        {
            PlayerPrefs.DeleteKey(_key + _id);
            PlayerPrefs.Save();
        }
    }
}
using UnityEngine;

namespace wzebra.kit.sounds
{
    public class SoundsPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;

        [SerializeField] private bool _separate;

        [SerializeField] private AudioClip[] _clips;

        private AudioSource[] _sources;

        private void Start()
        {
            if (_separate)
            {
                _sources = new AudioSource[_clips.Length];

                for (int i = 0; i < _clips.Length; i++)
                {
                    GameObject go = new GameObject();
                    go.transform.SetParent(transform);
                    go.name = _clips[i].name + "Player";

                    _sources[i] = go.AddComponent<AudioSource>();
                    _sources[i].outputAudioMixerGroup = _source.outputAudioMixerGroup;
                }
            }
        }

        public void Play(int index)
        {
            AudioSource source = _separate ? _sources[index] : _source;

            source.clip = _clips[index];
            source.Play();
        }
    }
}
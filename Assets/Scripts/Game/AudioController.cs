using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class AudioController : MonoBehaviour
    {
        public AudioSource fly, scream, window, kebab, breathing, escaping, music;
        public AudioClip[] flyNoises;
        public AudioClip screamKebab;
        public GameObject screamRooms;

        private float _musicfadeInDuration = 9f;
        private float _elapsedTime;

        private void Start()
        {
            screamRooms.SetActive(false);
        }

        public void PlayChefBreathing()
        {
            breathing.Play();
        }
        public void WindowBang()
        {
            window.Play();
        }

        public void PlayEarlyScream()
        {
            scream.Play();
        }

        public void PlayFlyNoise(int index)
        {
            fly.clip = flyNoises[index];
            fly.Play();
        }

        public void RevealScreamRooms()
        {
            screamRooms.SetActive(true);
        }

        public void SwitchToKebabMan()
        {
            kebab.Play();
        }

        public void PlayEscapeScream()
        {
            escaping.Play();
        }

        public void SetFadeInMusic()
        {
            StartCoroutine(FadeInMusic());
        }

        private IEnumerator FadeInMusic()
        {
            music.Play();
            _elapsedTime = 0;
            while (_elapsedTime < _musicfadeInDuration)
            {
                _elapsedTime += Time.deltaTime;

                music.volume = Mathf.Lerp(0, 0.8f, _elapsedTime / _musicfadeInDuration);
                yield return null;
            }

            music.volume = 1;
        }
    }
}

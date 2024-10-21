using System;
using UnityEngine;

namespace Game
{
    public class AudioController : MonoBehaviour
    {
        public AudioSource fly, scream, window, kebab;
        public AudioClip[] flyNoises;
        public AudioClip screamKebab;
        public GameObject screamRooms;

        private void Start()
        {
            screamRooms.SetActive(false);
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
            kebab.clip = screamKebab;
        }
    }
}

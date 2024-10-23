using System;
using UnityEngine;

namespace Intro
{
    public class UiAudio : MonoBehaviour
    {
        public AudioClip enter, exit, click;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            //MenuButton.OnButtonEvent += HandleButtonEvent;
        }

        private void OnDisable()
        {
            //MenuButton.OnButtonEvent -= HandleButtonEvent;
        }

        private void HandleButtonEvent(int eventId)
        {
            switch (eventId)
            {
                case 0:
                    PlayEnterSound();
                    break;
                case 1:
                    PlayExitSound();
                    break;
                case 2:
                    PlayClickSound();
                    break;
            }
        }
        
        public void PlayEnterSound()
        {
            _audioSource.volume = 0.7f;
            _audioSource.pitch = 0.9f;
            _audioSource.clip = enter;
            _audioSource.Play();
        }

        public void PlayExitSound()
        {
            _audioSource.volume = 0.4f;
            _audioSource.pitch = 0.6f;
            _audioSource.clip = exit;
            _audioSource.Play();
        }

        public void PlayClickSound()
        {
            _audioSource.volume = 1;
            _audioSource.pitch = 1;
            _audioSource.clip = click;
            _audioSource.Play();
        }
    }
}

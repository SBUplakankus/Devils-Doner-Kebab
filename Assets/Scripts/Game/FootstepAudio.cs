using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class FootstepAudio : MonoBehaviour
    {
        private NavMeshAgent _navMesh;
        private AudioSource _audioSource;

        private const float StepInterval = 0.75f;
        private float _nextStepTime = 0;
    
        private void Awake()
        {
            _navMesh = GetComponent<NavMeshAgent>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            var velocity = _navMesh.velocity.magnitude;
            if(velocity < 0.5f) return;
            _nextStepTime += Time.deltaTime;
            if (_nextStepTime < StepInterval) return;
            PlayFootstep();
        }

        private void PlayFootstep()
        {
            _audioSource.pitch = UnityEngine.Random.Range(0.55f, 0.65f);
            _audioSource.Play();
            _nextStepTime = 0;
        }
    }
}

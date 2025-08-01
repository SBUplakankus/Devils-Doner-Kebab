using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public static event Action OnMovementEnd;
        
        [Header("Components")]
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private AudioSource _audioSource;
        
        [Header("Movement")]
        private Transform _currentTarget;

        [Header("Looking")] 
        public Transform player;
        
        [Header("Variables")]
        private bool _isMoving;
        private bool _isLooking;
        public bool isLeaving;
        public bool isAttacking;
        private float _rotationSpeed = 5f;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _isMoving = false;
            isAttacking = false;
        }

        private void Update()
        {
            if (_isMoving)
            {
                _navMeshAgent.SetDestination(_currentTarget.position);
                var distance = Vector3.Distance(transform.position, _currentTarget.position);
                if (distance < 0.25f && !isAttacking)
                {
                    if(isLeaving)
                        RemoveNpc();
                    else
                    {
                        _animator.SetBool("Walking", false);
                        OnMovementEnd?.Invoke();
                        _isMoving = false;
                    }
                }
                else if (distance < 2f && isAttacking)
                {
                    OnMovementEnd?.Invoke();
                }
            }
            else if (_isLooking)
            {
                var direction = player.position - transform.position;
                var targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
            
                
        }

        public void PlayNpcScream()
        {
            _audioSource.Play();
        }
        
        /// <summary>
        /// Spawn in the NPC
        /// </summary>
        /// <param name="spawnPoint">Spawn Point</param>
        public void SpawnNpc(Transform spawnPoint)
        {
            transform.position = spawnPoint.position;
        }
        
        /// <summary>
        /// Remove the NPC
        /// </summary>
        public void RemoveNpc()
        {
            gameObject.SetActive(false);
            Debug.Log("Deactivate");
        }
        
        /// <summary>
        /// Update the movement position of the camera
        /// </summary>
        /// <param name="movePoint">ID of the next Position</param>
        public void UpdateMovePosition(Transform movePoint)
        {
            _animator.SetBool("Walking", true);
            _isLooking = false;
            _navMeshAgent.enabled = true;
            _navMeshAgent.destination = movePoint.position;
            _currentTarget = movePoint;
            _isMoving = true;
        }
        
        /// <summary>
        /// Make the camera look at a target
        /// </summary>
        public void LookAtPlayer()
        {
            _isLooking = true;
            _navMeshAgent.enabled = false;
        }
    
}

using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static event Action OnMovementEnd;
        
        [Header("Components")]
        private NavMeshAgent _navMeshAgent;
        
        [Header("Movement")]
        public Transform[] movePoints;
        private Transform _currentTarget;
        
        [Header("Looking")]
        public Transform[] lookTargets;
        private Transform _currentLook;
        
        [Header("Variables")]
        private bool _isMoving;
        private bool _isLooking;
        public float rotationSpeed = 1f;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            transform.position = movePoints[0].position;
            _isMoving = false;
        }

        private void Update()
        {
            if (_isMoving)
            {
                _navMeshAgent.SetDestination(_currentTarget.position);
                var distance = Vector3.Distance(transform.position, _currentTarget.position);
                if (distance < 0.1f)
                {
                    OnMovementEnd?.Invoke();
                    _isMoving = false;
                }
            }
            else if (_isLooking)
            {
                var direction = _currentLook.position - transform.position;
                var targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            
                
        }
        
        /// <summary>
        /// Update the movement position of the camera
        /// </summary>
        /// <param name="pos">ID of the next Position</param>
        public void UpdateMovePosition(int pos)
        {
            _isLooking = false;
            _navMeshAgent.enabled = true;
            _navMeshAgent.destination = movePoints[pos].position;
            _currentTarget = movePoints[pos];
            _isMoving = true;
        }
        
        /// <summary>
        /// Make the camera look at a target
        /// </summary>
        /// <param name="target">Target to Look At</param>
        public void LookAtTarget(int target)
        {
            _isLooking = true;
            _navMeshAgent.enabled = false;
            _currentLook = lookTargets[target];
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static event Action OnMovementEnd;
        
        [Header("Components")]
        private NavMeshAgent _navMeshAgent;
        
        [Header("Movement")]
        public Transform[] movePoints;
        public Transform currentTarget;
        
        [Header("Looking")]
        public Transform[] lookTargets;
        private Transform _currentLook;
        
        [Header("Variables")]
        private bool _isMoving;
        private bool _isLooking;
        private float _rotationSpeed = 5f;

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
                _navMeshAgent.SetDestination(currentTarget.position);
                var distance = Vector3.Distance(transform.position, currentTarget.position);
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
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
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
            currentTarget = movePoints[pos];
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

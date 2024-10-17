using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        private NavMeshAgent _navMeshAgent;
        public Transform[] movePoints;
        public Transform currentTarget;
        
        [Header("Variables")]
        private bool _isMoving;
        private float _rotationSpeed = 5f;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            transform.position = movePoints[0].position;
            _isMoving = false;
            UpdateMovePosition(1);
        }

        private void Update()
        {
            if (!_isMoving) return;
            _navMeshAgent.SetDestination(currentTarget.position);
            if (_navMeshAgent.remainingDistance < 0.1f)
                _isMoving = false;
        }
        
        /// <summary>
        /// Update the movement position of the camera
        /// </summary>
        /// <param name="pos">ID of the next Position</param>
        private void UpdateMovePosition(int pos)
        {
            _navMeshAgent.destination = movePoints[pos].position;
            currentTarget = movePoints[pos];
            _isMoving = true;
        }
        
        /// <summary>
        /// Make the camera look at a target
        /// </summary>
        /// <param name="target">Target to Look At</param>
        public void LookAtTarget(Transform target)
        {
            var direction = target.position - transform.position;
            direction.y = 0;

            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}

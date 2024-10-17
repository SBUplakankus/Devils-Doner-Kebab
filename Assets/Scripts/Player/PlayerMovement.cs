using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Stats")]
        public float moveSpeed;
        private float _horizontalInput;
        private float _verticalInput;
        
        [Header("Components")] 
        public Transform orientation;
        private Vector3 _moveDirection;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void Update()
        {
            GetUserInput();
        }

        private void GetUserInput()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
        }

        private void HandleMovement()
        {
            _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
            _rb.AddForce(_moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
    }
}

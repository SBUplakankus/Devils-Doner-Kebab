using System;
using UnityEngine;

namespace Player
{
    public class CameraMovement : MonoBehaviour
    {
        public Transform cameraPosition;

        private void LateUpdate()
        {
            transform.position = cameraPosition.position;
        }
    }
}

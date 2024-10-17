using System;
using UnityEngine;

namespace Systems
{
    public class LightController : MonoBehaviour
    {
        public Light[] shopFront;
        public Light[] kitchen;
        public Light freezer;
        public Light storage;

        private void OnEnable()
        {
            GameDirector.OnNextEvent += HandleNextEvent;
        }

        private void OnDisable()
        {
            GameDirector.OnNextEvent -= HandleNextEvent;
        }

        private void HandleNextEvent(int eventId)
        {
            
        }
    }
}

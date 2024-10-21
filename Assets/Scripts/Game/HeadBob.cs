using System;
using UnityEngine;

namespace Game
{
    public class HeadBob : MonoBehaviour
    {
        public float amount = 0.002f;
        public float frequency = 10f;
        public float smooth = 10f;

        private void Start()
        {
            ;
            UpdateHeadBobState(0);
        }

        private void Update()
        {
            StartHeadBob();
        }

        private Vector3 StartHeadBob()
        {
            var pos = Vector3.zero;
            pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
            transform.localPosition += pos;

            return pos;
        }

        public void UpdateHeadBobState(int id)
        {
            if (id == 0)
            {
                amount = 0.003f;
                frequency = 2;
                smooth = 6;
            }
            else
            {
                amount = 0.012f;
                frequency = 7;
                smooth = 10;
            }
        }
    }
}

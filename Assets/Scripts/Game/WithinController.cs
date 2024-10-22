using System;
using UnityEngine;

namespace Game
{
    public class WithinController : MonoBehaviour
    {
        public GameObject normal, fucked;
        private void Start()
        {
            normal.SetActive(true);
            fucked.SetActive(false);
        }

        public void RevealWithin()
        {
            normal.SetActive(false);
            fucked.SetActive(true);
        }
    }
}

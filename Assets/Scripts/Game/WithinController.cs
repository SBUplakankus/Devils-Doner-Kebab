using System;
using UnityEngine;

namespace Game
{
    public class WithinController : MonoBehaviour
    {
        public GameObject[] horrorAudio;
        public GameObject[] kebabs;

        private void Start()
        {
            kebabs[0].SetActive(true);
            kebabs[1].SetActive(false);
            
            foreach(var noise in horrorAudio)
            {
                noise.SetActive(false);
            }
        }

        public void RevealWithin()
        {
            kebabs[0].SetActive(false);
            kebabs[1].SetActive(true);
            
            foreach(var noise in horrorAudio)
            {
                noise.SetActive(true);
            }
        }
    }
}

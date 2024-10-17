using UnityEngine;

namespace Systems
{
    public class NpcSystem : MonoBehaviour
    {
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

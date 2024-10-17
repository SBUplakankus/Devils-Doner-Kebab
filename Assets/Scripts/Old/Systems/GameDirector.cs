using System;
using UnityEngine;

namespace Systems
{
    public class GameDirector : MonoBehaviour
    {
        public static event Action<int> OnNextEvent;
        
        [Header("Game Details")]
        private int _currentStage;
        
        /// <summary>
        /// Progress the game to the next stage
        /// </summary>
        public void SetNextEvent()
        {
            OnNextEvent?.Invoke(_currentStage);
            _currentStage++;
        }
    }
}

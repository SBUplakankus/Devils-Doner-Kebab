using System;
using Dialogue;
using PrimeTween;
using UnityEngine;

namespace Player
{
    public class GameController : MonoBehaviour
    {
        public PlayerController player;
        public DialogueController dialogue;

        public int nextEventId;
        
        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        private void EventOne()
        {
            dialogue.StartConversation(0);
        }
    }
}

using System;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        public static event Action OnConversationStart;
        public static event Action<string> OnInteractionEnter;
        public static event Action OnInteractionExit;
            
        public enum LookingAt {None, Npc, Devil, Pickup, Place}
        
        [Header("Checks")] 
        private LookingAt _lookingAt;
        private Transform _currentTarget;

        [Header("Interact Objects")] 
        private ConvoSO _nextConvo;
            
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                switch (_lookingAt)
                {
                    case LookingAt.Npc:
                        StartConversation();
                        break;
                    case LookingAt.Devil:
                        break;
                    case LookingAt.Pickup:
                        break;
                    case LookingAt.Place:
                        break;
                    case LookingAt.None:
                        break;
                    default:
                        break;
                }
            }
        }
        private void HandleInteractionEnter(string interactionText)
        {
            OnInteractionEnter?.Invoke(interactionText);
        }
    
        private void HandleInteractionExit()
        {
            _lookingAt = LookingAt.None;
            OnInteractionExit?.Invoke();
        }
            
        private void StartConversation()
        {
            OnInteractionExit?.Invoke();
            OnConversationStart?.Invoke();
        }
    }
}

using System;
using Player;
using TMPro;
using UnityEngine;

namespace Systems
{
    public class InteractionDisplay : MonoBehaviour
    {
        
        [Header("Text Display")]
        public TMP_Text interactText;

        [Header("Interact Info")] 
        private bool _ableToInteract;

        private void Start()
        {
            HideInteractText();
        }

        private void OnEnable()
        {
            PlayerInteraction.OnInteractionEnter += ShowInteractText;
            PlayerInteraction.OnInteractionExit += HideInteractText;
        }

        private void OnDisable()
        {
            PlayerInteraction.OnInteractionEnter -= ShowInteractText;
            PlayerInteraction.OnInteractionExit -= HideInteractText;
        }

        /// <summary>
        /// Show the interaction pop up
        /// </summary>
        /// <param name="text">Text to display</param>
        private void ShowInteractText(string text)
        {
            interactText.gameObject.SetActive(true);
            interactText.text = text + " [E]";
        }
        
        /// <summary>
        /// Hide the interact pop up
        /// </summary>
        private void HideInteractText()
        {
            interactText.gameObject.SetActive(false);
        }
    }
}

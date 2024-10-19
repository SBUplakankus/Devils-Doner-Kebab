using System;
using TMPro;
using UnityEngine;

namespace Player
{
    public class ChoiceController : MonoBehaviour
    {
        public static event Action<int> OnChoiceMade; 
        
        public GameObject choiceDisplay;
        public TMP_Text choiceName, option1, option2;
        private bool _waitingOnChoice;

        private void Start()
        {
            HideChoiceDisplay();
        }

        private void Update()
        {
            if(!_waitingOnChoice) return;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("1");
                OnChoiceMade?.Invoke(0);
                HideChoiceDisplay();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("2");
                OnChoiceMade?.Invoke(1);
                HideChoiceDisplay();
            }
        }

        /// <summary>
        /// Display the choice options and set them
        /// </summary>
        /// <param name="optionDesc">Choice Description</param>
        /// <param name="optionOne">First Choice</param>
        /// <param name="optionTwo">Second Choice</param>
        public void SetChoices(string optionDesc, string optionOne, string optionTwo)
        {
            choiceDisplay.SetActive(true);
            choiceName.text = optionDesc;
            option1.text = "1. " + optionOne;
            option2.text = "2. " + optionTwo;
            _waitingOnChoice = true;
        }
        
        /// <summary>
        /// Hide the choice display
        /// </summary>
        private void HideChoiceDisplay()
        {
            _waitingOnChoice = false;
            choiceDisplay.SetActive(false);
        }
    }
}

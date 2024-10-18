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

        private int _gameStage;
        
        private void Start()
        {
            _gameStage = 0;
            ProgressGame();
        }

        private void OnEnable()
        {
            DialogueController.OnDialogueEnd += ProgressGame;
            PlayerController.OnMovementEnd += ProgressGame;
        }

        private void OnDisable()
        {
            DialogueController.OnDialogueEnd -= ProgressGame;
            DialogueController.OnDialogueEnd -= ProgressGame;
        }

        private void ProgressGame()
        {
            switch (_gameStage)
            {
                case 0:
                    EventOne();
                    break;
                case 1:
                    EventTwo();
                    break;
                case 2:
                    EventThree();
                    break;
                case 3:
                    EventFour();
                    break;
                case 4:
                    EventFive();
                    break;
            }

            _gameStage++;
            Debug.Log(_gameStage);
        }
        
        private void EventOne()
        {
            dialogue.StartConversation(0);
        }

        private void EventTwo()
        {
            player.UpdateMovePosition(1);
        }

        private void EventThree()
        {
            player.LookAtTarget(0);
            dialogue.StartConversation(1);
        }

        private void EventFour()
        {
            player.UpdateMovePosition(2);
        }

        private void EventFive()
        {
            player.LookAtTarget(1);
        }
    }
}

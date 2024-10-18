using System;
using Dialogue;
using PrimeTween;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class GameController : MonoBehaviour
    {
        private enum ChoiceMade {Option1, Option2}
        
        public PlayerController player;
        public DialogueController dialogue;
        public ChoiceController choice;

        private ChoiceMade _orderChoice;
        private ChoiceMade _drinkChoice;
        private ChoiceMade _meetingChoice;
        

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
            ChoiceController.OnChoiceMade += HandleChoiceMade;
        }

        private void OnDisable()
        {
            DialogueController.OnDialogueEnd -= ProgressGame;
            DialogueController.OnDialogueEnd -= ProgressGame;
            ChoiceController.OnChoiceMade -= HandleChoiceMade;
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
                case 5:
                    switch (_orderChoice)
                    {
                        case ChoiceMade.Option1:
                            GetShot();
                            break;
                        case ChoiceMade.Option2:
                            EventSix();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
            }

            _gameStage++;
        }

        private void HandleChoiceMade(int choice)
        {
            switch (_gameStage)
            {
                case 6:
                    if (choice == 0)
                        _orderChoice = ChoiceMade.Option1;
                    else
                        _orderChoice = ChoiceMade.Option2;
                    break;
            }
            ProgressGame();
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
            choice.SetChoices("What will you order?", "Doner Kebab", "Garlic Cheese Chips");
        }

        private void EventSix()
        {
            dialogue.StartConversation(2);
        }

        private void GetShot()
        {
            dialogue.StartConversation(3);
        }
        
        private void EventSeven()
        {
            
        }
    }
}

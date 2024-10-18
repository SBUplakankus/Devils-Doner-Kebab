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
        public NpcSystem npc;

        private ChoiceMade _orderChoice;
        private ChoiceMade _drinkChoice;
        private ChoiceMade _weirdoChoice;
        

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
            NpcController.OnMovementEnd += ProgressGame;
        }

        private void OnDisable()
        {
            DialogueController.OnDialogueEnd -= ProgressGame;
            DialogueController.OnDialogueEnd -= ProgressGame;
            ChoiceController.OnChoiceMade -= HandleChoiceMade;
            NpcController.OnMovementEnd -= ProgressGame;
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
                case 6:
                    EventSeven();
                    break;
                case 7:
                    EventEight();
                    break;
                case 8:
                    EventNine();
                    break;
                case 9:
                    EventTen();
                    break;
                case 10:
                    switch (_weirdoChoice)
                    {
                        case ChoiceMade.Option1:
                            EventEleven();
                            break;
                        case ChoiceMade.Option2:
                            GetHarvested();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case 11:
                    EventTwelve();
                    break;
            }

            _gameStage++;
        }

        private void HandleChoiceMade(int choiceInt)
        {
            switch (_gameStage)
            {
                case 6:
                    _orderChoice = choiceInt == 0 ? ChoiceMade.Option1 : ChoiceMade.Option2;
                    break;
                case 11:
                    _weirdoChoice = choiceInt == 0 ? ChoiceMade.Option1 : ChoiceMade.Option2;
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
            player.UpdateMovePosition(3);
            npc.SpawnNpc(0,0);
        }

        private void EventEight()
        {
            npc.MoveNpc(0, 2);
            player.LookAtTarget(2);
        }

        private void EventNine()
        {
            dialogue.StartConversation(4);
            npc.LookAtPlayer(0);
            player.LookAtTarget(3);
        }

        private void EventTen()
        {
            choice.SetChoices("Let this goon harvest your organs", "Yea why not", "Tell him to fuck off.");
        }

        private void GetHarvested()
        {
            dialogue.StartConversation(6);
        }

        private void EventEleven()
        {
            dialogue.StartConversation(5);
        }

        private void EventTwelve()
        {
            npc.MoveNpc(0, 4);
            player.LookAtTarget(3);
        }
    }
}

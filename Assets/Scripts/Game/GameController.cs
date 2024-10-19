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
        private ChoiceMade _offerChoice;
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
                case 12:
                    EventThirteen();
                    break;
                case 13:
                    EventFourteen();
                    break;
                case 14:
                    EventFifteen();
                    break;
                case 15:
                    EventSixteen();
                    break;
                case 16:
                    EventSeventeen();
                    break;
                case 17:
                    EventEighteen();
                    break;
                case 18:
                    EventNineteen();
                    break;
                case 19:
                    switch (_weirdoChoice)
                    {
                        case ChoiceMade.Option1:
                            EventTwenty();
                            break;
                        case ChoiceMade.Option2:
                            SeeWithin();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
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
                case 19:
                    _offerChoice = choiceInt == 0 ? ChoiceMade.Option1 : ChoiceMade.Option2;
                    break;
            }
            ProgressGame();
        }

        #region Intro
        private void EventOne()
        {
            player.rotationSpeed = 1f;
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
        #endregion

        #region Order
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
        #endregion
        
        #region Weirdo
        private void EventEight()
        {
            npc.MoveNpc(0, 2);
            player.LookAtTarget(2);
        }

        private void EventNine()
        {
            player.rotationSpeed = 3f;
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
            //Weirdo Leaves Scene 
            npc.MoveNpc(0, 0);
            npc.SpawnNpc(3,1);
        }

        private void EventThirteen()
        {
            player.rotationSpeed = 1f;
            player.LookAtTarget(1);
            //Look to Order man and ask when ready
            npc.SetNpcToLeaving(0);
            npc.MoveNpc(0, 5);
            dialogue.StartConversation(7);
        }
        #endregion

        #region Offer
        private void EventFourteen()
        {
            //Look at devil man
            player.LookAtTarget(4);
            dialogue.StartConversation(8);
            npc.SpawnNpc(1,2);
            
        }
        
        private void EventFifteen()
        {
            //Move to window
            npc.MoveNpc(1,3);
        }

        private void EventSixteen()
        {
            npc.LookAtPlayer(1);
            player.rotationSpeed = 4f;
            //Bang on window see offer man
            player.LookAtTarget(5);
            dialogue.StartConversation(9);
        }

        private void EventSeventeen()
        {
            //Offer man comes inside
            npc.MoveNpc(1, 4);
        }

        private void EventEighteen()
        {
            //Offer convo
            npc.LookAtPlayer(1);
            dialogue.StartConversation(10);
        }

        private void EventNineteen()
        {
            choice.SetChoices("See what lies beneath", "Sounds cool", "Nah I'm good");
        }

        private void EventTwenty()
        {
            Debug.Log("Progress 20");
        }

        private void SeeWithin()
        {
            Debug.Log("See Within");
        }
        #endregion
    }
}

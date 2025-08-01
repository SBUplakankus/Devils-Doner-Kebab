using System;
using Dialogue;
using Game;
using UnityEngine;

namespace Player
{
    public class GameController : MonoBehaviour
    {
        private enum ChoiceMade
        {
            Option1,
            Option2
        }
        
        private enum Ending{None, Chips, Weirdo, Offer, Heretic, Normal}

        public PlayerController player;
        public DialogueController dialogue;
        public ChoiceController choice;
        public NpcSystem npc;
        public EndScreenController endScreen;
        public AudioController audioCon;
        public HeadBob headBob;
        public LightController lights;

        private ChoiceMade _orderChoice;
        private ChoiceMade _offerChoice;
        private ChoiceMade _weirdoChoice;
        private ChoiceMade _pickupChoice;

        private Ending _ending;

        private int _gameStage;
        private int _endingStage;

        private void Start()
        {
            _gameStage = 0;
            _endingStage = -1;
            ProgressGame();
            _ending = Ending.None;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            DialogueController.OnDialogueEnd += ProgressGame;
            PlayerController.OnMovementEnd += ProgressGame;
            ChoiceController.OnChoiceMade += HandleChoiceMade;
            NpcController.OnMovementEnd += ProgressGame;
            EndScreenController.OnBlinkEnd += ProgressGame;
        }

        private void OnDisable()
        {
            DialogueController.OnDialogueEnd -= ProgressGame;
            PlayerController.OnMovementEnd -= ProgressGame;
            ChoiceController.OnChoiceMade -= HandleChoiceMade;
            NpcController.OnMovementEnd -= ProgressGame;
            EndScreenController.OnBlinkEnd -= ProgressGame;
        }

        private void ProgressGame()
        {
            switch (_ending)
            {
                case Ending.None:
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
                                EventSix();
                                break;
                            case ChoiceMade.Option2:
                                _ending = Ending.Chips;
                                GetShot();
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
                                GetHarvested();
                                _ending = Ending.Weirdo;
                                break;
                            case ChoiceMade.Option2:
                                EventEleven();
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
                        switch (_offerChoice)
                        {
                            case ChoiceMade.Option1:
                                WithinReaction();
                                _ending = Ending.Offer;
                                break;
                            case ChoiceMade.Option2:
                                EventTwenty();
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                        case 20:
                            EventTwentyOne();
                            break;
                        case 21: 
                            EventTwentyTwo();
                            break;
                        case 22:
                            EventTwentyThree();
                            break;
                        case 23:
                            EventTwentyFour();
                            break;
                        case 24:
                        switch (_orderChoice)
                        {
                            case ChoiceMade.Option1:
                                EventTwentyFive();
                                _ending = Ending.Normal;
                                break;
                            case ChoiceMade.Option2:
                                RefuseKebab();
                                _ending = Ending.Heretic;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                }
                    break;
                
                    case Ending.Chips:
                    switch (_endingStage)
                    {
                        case 0:
                            ChipsAttack();
                            break;
                        case 1:
                            ChipsEnding();
                            break;
                    }
                    break;
                    
                    case Ending.Weirdo:
                    switch (_endingStage)
                    {
                        case 0:
                            WeirdoAttack();
                            break;
                        case 1:
                            WeirdoEnding();
                            break;
                    }
                    break;
                    
                    case Ending.Offer:
                    switch (_endingStage)
                    {
                        case 0:
                            SeeWithin();
                            break;
                        case 1:
                            WithinReveal();
                            break;
                        case 2:
                            WithinMoveToDemon();
                            break;
                        case 3:
                            WithinAsk();
                            break;
                        case 4:
                            WithinMoveToOrder();
                            break;
                        case 5:
                            WithinPickup();
                            break;
                        case 6:
                            WithinLeave();
                            break;
                        case 7:
                            WithinEnding();
                            break;
                    }
                    break;
                    
                    case Ending.Heretic:
                    switch (_endingStage)
                    {
                        case 0:
                            HereticAppearance();
                            break;
                        case 1:
                            HereticAttack();
                            break;
                        case 2:
                            HereticEnding();
                            break;
                    }
                    break;
                    
                    case Ending.Normal:
                    switch (_endingStage)
                    {
                        case 0:
                            EventTwentySix();
                            break;
                        case 1:
                            MainLineEnding();
                            break;
                    }
                    break;
            }

            if (_ending == Ending.None)
                _gameStage++;
            else
                _endingStage++;
        }

        private void HandleChoiceMade(int choiceInt)
        {
            switch (_gameStage)
            {
                case 5:
                    _orderChoice = choiceInt == 0 ? ChoiceMade.Option1 : ChoiceMade.Option2;
                    break;
                case 10:
                    _weirdoChoice = choiceInt == 0 ? ChoiceMade.Option1 : ChoiceMade.Option2;
                    break;
                case 19:
                    _offerChoice = choiceInt == 0 ? ChoiceMade.Option1 : ChoiceMade.Option2;
                    break;
                case 24:
                    _orderChoice = choiceInt == 0 ? ChoiceMade.Option1 : ChoiceMade.Option2;
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
            headBob.UpdateHeadBobState(1);
            player.UpdateMovePosition(1);
        }

        private void EventThree()
        {
            headBob.UpdateHeadBobState(0);
            player.LookAtTarget(0);
            dialogue.StartConversation(1);
        }

        private void EventFour()
        {
            headBob.UpdateHeadBobState(1);
            player.LookAtTarget(1);
            player.UpdateMovePosition(2);
            audioCon.PlayFlyNoise(1);
        }

        #endregion

        #region Order

        private void EventFive()
        {
            headBob.UpdateHeadBobState(0);
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
            audioCon.PlayChefBreathing();
            npc.SpawnNpc(3,4);
            npc.LookAtPlayer(3);
        }

        private void EventSeven()
        {
            headBob.UpdateHeadBobState(1);
            npc.DisableDemonChefNav();
            player.UpdateMovePosition(3);
            npc.SpawnNpc(0, 0);
        }

        #endregion

        #region Weirdo

        private void EventEight()
        {
            headBob.UpdateHeadBobState(0);
            npc.MoveNpc(0, 2);
            player.LookAtTarget(2);
            audioCon.PlayFlyNoise(2);
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
            choice.SetChoices("Let this goon harvest your organs", "Yea why not", "Fuck off.");
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
            npc.SpawnNpc(3, 1);
            npc.LookAtPlayer(3);
        }

        private void EventThirteen()
        {
            player.rotationSpeed = 1f;
            player.LookAtTarget(1);
            //Look to Order man and ask when ready
            npc.SetNpcToLeaving(0);
            npc.MoveNpc(0, 5);
            dialogue.StartConversation(7);
            npc.DeleteNpc(0);
            audioCon.PlayEarlyScream();
        }

        #endregion

        #region Offer

        private void EventFourteen()
        {
            //Look at devil man
            player.LookAtTarget(4);
            dialogue.StartConversation(8);
            npc.SpawnNpc(1, 2);

        }

        private void EventFifteen()
        {
            //Move to window
            npc.MoveNpc(1, 3);
        }

        private void EventSixteen()
        {
            npc.LookAtPlayer(1);
            player.rotationSpeed = 4f;
            //Bang on window see offer man
            audioCon.WindowBang();
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
            audioCon.PlayFlyNoise(1);
            npc.LookAtPlayer(1);
            dialogue.StartConversation(10);
        }

        private void EventNineteen()
        {
            choice.SetChoices("See what lies beneath", "Sounds cool.", "Nah I'm good.");
        }

        private void EventTwenty()
        {
            //Dont wanna see within
            dialogue.StartConversation(11);
            audioCon.PlayEscapeScream();
        }

        private void EventTwentyOne()
        {
            //Mystery Man Leaves
            npc.MoveNpc(1, 0);
        }
        private void SeeWithin()
        {
            endScreen.SetPlayerBlink();
        }

        private void WithinReaction()
        {
            dialogue.StartConversation(12);
        }
        #endregion
        
        #region Pickup Order
        private void EventTwentyTwo()
        {
            //Get told order is ready
            player.rotationSpeed = 1f;
            player.LookAtTarget(1);
            dialogue.StartConversation(13);
        }

        private void EventTwentyThree()
        {
            headBob.UpdateHeadBobState(1);
            //Move to counter
            player.UpdateMovePosition(2);
            npc.DeleteNpc(1);
            audioCon.PlayFlyNoise(4);
        }
        
        private void EventTwentyFour()
        {
            headBob.UpdateHeadBobState(0);
            player.LookAtTarget(1);
            choice.SetChoices("Take the Doner Kebab", "Thank you boss.", "Changed my mind..");
        }

        private void EventTwentyFive()
        {
            //Thanks for kebab
            dialogue.StartConversation(14);
        }

        private void RefuseKebab()
        {
            dialogue.StartConversation(15);
            npc.SpawnNpc(4,3);
        }

        private void EventTwentySix()
        {
            audioCon.SetFadeInMusic();
            headBob.UpdateHeadBobState(1);
            //Leave the kebab shop 
            player.UpdateMovePosition(1);
        }

        
        
        #endregion

        #region Endings
        
    
        private void MainLineEnding()
        {
            endScreen.DisplayEndScreen("Ending One", "You ordered a Doner Kebab after a great night out on the drink.");
        }

        private void ChipsAttack()
        {
            npc.NpcAttacking(3);
            npc.PlayNpcScream(3);
            player.LookAtTarget(4);
            npc.MoveNpc(3, 6);
        }
        
        private void ChipsEnding()
        {
            endScreen.DisplayEndScreen("Ending Three", "You ordered Garlic Cheese Chips and got murdered by the kitchen staff.");
        }
        
        private void WeirdoAttack()
        {
            npc.NpcAttacking(0);
            npc.PlayNpcScream(0);
            npc.MoveNpc(0,6);
        }
        
        private void WeirdoEnding()
        {
            endScreen.DisplayEndScreen("Ending Four", "You let the strange man harvest your organs for his own personal pleasure.");
        }
        
        private void HereticAppearance()
        {
            npc.LookAtPlayer(4);
            player.rotationSpeed = 4f;
            player.LookAtTarget(6);
            dialogue.StartConversation(16);
        }

        private void HereticAttack()
        {
            npc.NpcAttacking(4);
            npc.PlayNpcScream(4);
            npc.MoveNpc(4, 6);
        }
        
        private void HereticEnding()
        {
            endScreen.DisplayEndScreen("Ending Five", "You were executed by the harbinger of the devils doner on accounts of heresy.");
        }

        private void WithinReveal()
        {
            lights.SetLightColour(1);
            dialogue.StartConversation(17);
            audioCon.RevealScreamRooms();
            audioCon.SwitchToKebabMan();
        }

        private void WithinMoveToDemon()
        {
            headBob.UpdateHeadBobState(1);
            player.UpdateMovePosition(5);
        }

        private void WithinAsk()
        {
            headBob.UpdateHeadBobState(0);
            npc.DeleteNpc(1);
            player.LookAtTarget(4);
            dialogue.StartConversation(18);
        }

        private void WithinMoveToOrder()
        {
            headBob.UpdateHeadBobState(1);
            player.UpdateMovePosition(2);
        }

        private void WithinPickup()
        {
            headBob.UpdateHeadBobState(0);
            player.LookAtTarget(1);
            dialogue.StartConversation(19);
        }

        private void WithinLeave()
        {
            audioCon.SetFadeInMusic();
            headBob.UpdateHeadBobState(1);
            player.UpdateMovePosition(1);
        }
        
        private void WithinEnding()
        {
            endScreen.DisplayEndScreen("Ending Two", "You saw the kebab shop for what it really was. Got a great doner though.");
        }
        #endregion
    
    }
}

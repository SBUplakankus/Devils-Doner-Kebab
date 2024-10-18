using System;
using Player;
using ScriptableObjects;
using Systems;
using TMPro;
using UnityEngine;

namespace Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        public static event Action OnDialogueEnd;
        
        [Header("Conversations")] 
        public ConvoSO[] conversations;
        private ConvoSO _currentConvo;
        private int _convoIndex;
        public bool isTalking;

        [Header("Components")] 
        public Color32[] textColours;
        private DialogueTypewriter _dialogueDisplay;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _dialogueDisplay = GetComponent<DialogueTypewriter>();
        }

        private void Start()
        {
            _currentConvo = conversations[0];
        }
        
        private void Update()
        {
            if (!isTalking) return;

            if (!Input.GetKeyDown(KeyCode.E)) return;
            
            switch (_dialogueDisplay.dialogueState)
            {
                case DialogueTypewriter.DialogueState.Typing:
                    _dialogueDisplay.SkipToEnd();
                    break;
                case DialogueTypewriter.DialogueState.Finished:
                    ContinueConvo();
                    break;
                default:
                    break;
            }
        }
        
        
        public void StartConversation(int id)
        {
            _currentConvo = conversations[id];
            isTalking = true;
            _convoIndex = 0;
            PlayNextDialogue();
        }

        private void EndConversation()
        {
            isTalking = false;
            _dialogueDisplay.HideDialogueText();
            _audioSource.Stop();
            OnDialogueEnd?.Invoke();
        }

        private void ContinueConvo()
        {
            _convoIndex++;
            if (_convoIndex >= _currentConvo.dialogueTree.Length)
            {
               EndConversation();
            }
            else
            {
                PlayNextDialogue();
            }
            
        }

        private void PlayNextDialogue()
        {
            _dialogueDisplay.SetNextText(_currentConvo.dialogueTree[_convoIndex]);
            _dialogueDisplay.dialogueText.color = textColours[_currentConvo.dialogueSpeaker[_convoIndex]];
            _audioSource.PlayOneShot(_currentConvo.dialogueAudio[_convoIndex]);
        }
    }
}

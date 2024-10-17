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
        [Header("Conversations")] 
        public ConvoSO[] conversations;
        private ConvoSO _currentConvo;
        private int _convoIndex;
        public bool isTalking;
        private int _gameIndex;

        [Header("Components")] 
        public Color32[] textColours;
        public DialogueTypewriter dialogueDisplay;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _currentConvo = conversations[0];
            _gameIndex = 0;
        }
        
        private void Update()
        {
            if (!isTalking) return;

            if (!Input.GetKeyDown(KeyCode.E)) return;
            
            switch (dialogueDisplay.dialogueState)
            {
                case DialogueTypewriter.DialogueState.Typing:
                    dialogueDisplay.SkipToEnd();
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
            _gameIndex++;
            isTalking = true;
            _convoIndex = 0;
            PlayNextDialogue();
        }

        private void EndConversation()
        {
            isTalking = false;
            dialogueDisplay.HideDialogueText();
            _audioSource.Stop();
            _gameIndex++;
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
            dialogueDisplay.SetNextText(_currentConvo.dialogueTree[_convoIndex]);
            dialogueDisplay.dialogueText.color = textColours[_currentConvo.dialogueSpeaker[_convoIndex]];
            _audioSource.PlayOneShot(_currentConvo.dialogueAudio[_convoIndex]);
        }
    }
}

using System;
using JetBrains.Annotations;
using PrimeTween;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dialogue
{
    public class DialogueTypewriter : MonoBehaviour
    {
        public enum DialogueState {None, Typing, Finished}

        [Header("Typewriter Settings")] 
        private float _charsPerSecond = 35f;
        private int _punctuationPause = 20;
        private bool _textPlaying;
        private float _typeDuration;
        
        public TextMeshProUGUI dialogueText, ending, info;
        public DialogueState dialogueState;
        private AudioSource _audioSource;
        private Tween _dialogueTween;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            dialogueText.maxVisibleCharacters = 0;
        }
        public void SetNextText(string text)
        {
            dialogueText.text = text;
            dialogueState = DialogueState.Typing;
            AnimateText();
        }

        public void SetEndingText()
        {
            AnimateEndingText();
        }

        private Tween AnimateEndingText()
        {
            _charsPerSecond = 25;
            var endingAnim = TypewriterAnimationWithPunctuations(ending)
                .OnComplete(() => TypewriterAnimationWithPunctuations(info));
            return endingAnim;
        }
        
        private Tween AnimateText()
        {
            _dialogueTween = TypewriterAnimationWithPunctuations(dialogueText).OnComplete(() => dialogueState = DialogueState.Finished);
            return _dialogueTween;
        }
        
        /// <summary>
        /// Skip to the end of the dialogue typing
        /// </summary>
        public void SkipToEnd()
        {
            _dialogueTween.Complete();
            dialogueState = DialogueState.Finished;
            _audioSource.Stop();
        }
        
        /// <summary>
        /// Hide the dialogue text
        /// </summary>
        public void HideDialogueText()
        {
            dialogueState = DialogueState.None;
            dialogueText.maxVisibleCharacters = 0;
        }
        
        #region TypewriterAnimationWithPunctuations
        /// <summary>Typewriter animation which inserts pauses after punctuation marks.</summary>
        private Tween TypewriterAnimationWithPunctuations(TMP_Text text) {
            text.ForceMeshUpdate();
            RemapWithPunctuations(text, int.MaxValue, out int remappedCount, out _);
            var duration = remappedCount / _charsPerSecond;
            return Tween.Custom(this, 0f, remappedCount, duration, (t, x) => t.UpdateMaxVisibleCharsWithPunctuation(x, text), Ease.Linear);
        }

        private void UpdateMaxVisibleCharsWithPunctuation(float progress, TMP_Text text)
        {
            var remappedEndIndex = Mathf.RoundToInt(progress);
            RemapWithPunctuations(text, remappedEndIndex, out _, out int visibleCharsCount);
            if (text.maxVisibleCharacters != visibleCharsCount) {
                text.maxVisibleCharacters = visibleCharsCount;
                // play keyboard typing sound here if needed
            }
        }

        private void RemapWithPunctuations([NotNull] TMP_Text text, int remappedEndIndex, out int remappedCount, out int visibleCharsCount)
        {
            remappedCount = 0;
            visibleCharsCount = 0;
            var count = text.textInfo.characterCount;
            var characterInfos = text.textInfo.characterInfo;
            for (var i = 0; i < count; i++) {
                if (remappedCount >= remappedEndIndex) {
                    break;
                }   
                remappedCount++;
                visibleCharsCount++;
                if (!IsPunctuationChar(characterInfos[i].character)) continue;
                var nextIndex = i + 1;
                if (nextIndex != count && !IsPunctuationChar(characterInfos[nextIndex].character)) {
                    // add pause after the last subsequent punctuation character
                    remappedCount += Mathf.Max(0, _punctuationPause);
                }
            }

            bool IsPunctuationChar(char c) {
                return ".,:;!?".IndexOf(c) != -1;
            }
        }
        #endregion
    }
}

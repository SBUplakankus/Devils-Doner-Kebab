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
        private const float CharsPerSecond = 35f;
        private const int PunctuationPause = 10;
        private bool _textPlaying;
        private float _typeDuration;
        
        [Header("Components")]
        public TextMeshProUGUI dialogueText;
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
        
        private Tween AnimateText()
        {
            _dialogueTween = TypewriterAnimationWithPunctuations().OnComplete(() => dialogueState = DialogueState.Finished);
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
        private Tween TypewriterAnimationWithPunctuations() {
            dialogueText.ForceMeshUpdate();
            RemapWithPunctuations(dialogueText, int.MaxValue, out int remappedCount, out _);
            var duration = remappedCount / CharsPerSecond;
            return Tween.Custom(this, 0f, remappedCount, duration, (t, x) => t.UpdateMaxVisibleCharsWithPunctuation(x), Ease.Linear);
        }

        private void UpdateMaxVisibleCharsWithPunctuation(float progress)
        {
            var remappedEndIndex = Mathf.RoundToInt(progress);
            RemapWithPunctuations(dialogueText, remappedEndIndex, out _, out int visibleCharsCount);
            if (dialogueText.maxVisibleCharacters != visibleCharsCount) {
                dialogueText.maxVisibleCharacters = visibleCharsCount;
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
                    remappedCount += Mathf.Max(0, PunctuationPause);
                }
            }

            bool IsPunctuationChar(char c) {
                return ".,:;!?".IndexOf(c) != -1;
            }
        }
        #endregion
    }
}

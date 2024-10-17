using System;
using JetBrains.Annotations;
using PrimeTween;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Dialogue
{
    public class DialogueTypewriter : MonoBehaviour
    {
        private enum DialogueState {None, Typing, Finished}
        
        [Header("Typewriter Settings")]
        public float charsPerSecond = 35f;
        public int punctuationPause = 20;
        private bool _textPlaying;
        private float _typeDuration;
        
        [Header("Components")]
        public TextMeshProUGUI dialogueText;
        private DialogueState _dialogueState;
        private Tween _dialogueTween;

        private void Awake()
        {
            dialogueText.maxVisibleCharacters = 0;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (_dialogueState)
                {
                    case DialogueState.None:
                        SetNextText();
                        break;
                    case DialogueState.Typing:
                        SkipToEnd();
                        break;
                    case DialogueState.Finished:
                        HideDialogueText();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void SetNextText()
        {
            _dialogueState = DialogueState.Typing;
            AnimateText();
        }
        
        private Tween AnimateText()
        {
            _dialogueTween = TypewriterAnimationWithPunctuations().OnComplete(() => _dialogueState = DialogueState.Finished);
            return _dialogueTween;
        }
        
        #region TypewriterAnimationWithPunctuations
        /// <summary>Typewriter animation which inserts pauses after punctuation marks.</summary>
        private Tween TypewriterAnimationWithPunctuations() {
            dialogueText.ForceMeshUpdate();
            RemapWithPunctuations(dialogueText, int.MaxValue, out int remappedCount, out _);
            var duration = remappedCount / charsPerSecond;
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
                    remappedCount += Mathf.Max(0, punctuationPause);
                }
            }

            bool IsPunctuationChar(char c) {
                return ".,:;!?".IndexOf(c) != -1;
            }
        }
        
        /// <summary>
        /// Skip to the end of the dialogue typing
        /// </summary>
        private void SkipToEnd()
        {
            _dialogueTween.Complete();
            _dialogueState = DialogueState.Finished;
        }
        
        /// <summary>
        /// Hide the dialogue text
        /// </summary>
        public void HideDialogueText()
        {
            _dialogueState = DialogueState.None;
            dialogueText.maxVisibleCharacters = 0;
        }
        #endregion
    }
}

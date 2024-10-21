using System;
using System.Collections;
using Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class EndScreenController : MonoBehaviour
    {
        public static event Action OnBlinkEnd;
        
        public DialogueTypewriter typewriter;
        public Image fadeScreen;
        public Image blinkScreen;
        public TMP_Text ending, description;
        private readonly float _fadeDuration = 0.5f;

        private void Start()
        {
            ending.maxVisibleCharacters = 0;
            description.maxVisibleCharacters = 0;
            fadeScreen.gameObject.SetActive(false);
        }

        public void DisplayEndScreen(string endingText, string descriptionText)
        {
            fadeScreen.gameObject.SetActive(true);
            ending.text = endingText;
            description.text = descriptionText;
            StartCoroutine(FadeInScreen());
        }

        private IEnumerator FadeInScreen()
        {
            var startColor = fadeScreen.color;
            startColor.a = 0f;

            var endColor = fadeScreen.color;
            endColor.a = 1f;

            var elapsedTime = 0f;

            while (elapsedTime < _fadeDuration)
            {
                elapsedTime += Time.deltaTime;

                fadeScreen.color = Color.Lerp(startColor, endColor, elapsedTime / _fadeDuration);

                yield return null;
            }

            fadeScreen.color = endColor;
            typewriter.SetEndingText();
            yield return new WaitForSeconds(9);
            SceneManager.LoadScene(0);
        }

        private IEnumerator Blink()
        {
            var startColor = blinkScreen.color;
            startColor.a = 0f;

            var endColor = fadeScreen.color;
            endColor.a = 1f;

            var elapsedTime = 0f;
            
            while (elapsedTime < _fadeDuration)
            {
                elapsedTime += Time.deltaTime;

                blinkScreen.color = Color.Lerp(startColor, endColor, elapsedTime / _fadeDuration);

                yield return null;
            }

            fadeScreen.color = endColor;
            yield return new WaitForSeconds(1);

            elapsedTime = 0f;
            while (elapsedTime < _fadeDuration)
            {
                elapsedTime += Time.deltaTime;

                blinkScreen.color = Color.Lerp(endColor, startColor, elapsedTime / _fadeDuration);

                yield return null;
            }
            
            OnBlinkEnd?.Invoke();
        }
    }
}

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Intro
{
    public class MenuController : MonoBehaviour
    {
        public GameObject credits;
        public Image blackScreen;
        public Image fadeScreen;
        public TMP_Text introText;
        private bool _creditsOpen;

        private const float FadeDuration = 1.5f;

        private void Start()
        {
            credits.SetActive(false);
            _creditsOpen = false;
            Cursor.visible = true;

        }

        public void PlayGame()
        {
            StartCoroutine(IntroScreen());
        }

        public void HandleCreditsDisplay()
        {
            _creditsOpen = !_creditsOpen;
            credits.SetActive(_creditsOpen);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private IEnumerator IntroScreen()
        {
            var startColor = blackScreen.color;
            startColor.a = 0f;

            var endColor = blackScreen.color;
            endColor.a = 1f;

            var elapsedTime = 0f;
            
            while (elapsedTime < FadeDuration)
            {
                elapsedTime += Time.deltaTime;

                blackScreen.color = Color.Lerp(startColor, endColor, elapsedTime / FadeDuration);

                yield return null;
            }
            
            fadeScreen.gameObject.SetActive(true);
            introText.gameObject.SetActive(true);
            introText.text = "Press [E] to Progress Dialogue";
            yield return new WaitForSeconds(0.5f);
            
            startColor = fadeScreen.color;
            startColor.a = 1f;

            endColor = fadeScreen.color;
            endColor.a = 0f;

            elapsedTime = 0f;
            
            while (elapsedTime < FadeDuration)
            {
                elapsedTime += Time.deltaTime;

                fadeScreen.color = Color.Lerp(startColor, endColor, elapsedTime / FadeDuration);

                yield return null;
            }
            
            yield return new WaitForSeconds(1f);
            elapsedTime = 0f;
            while (elapsedTime < FadeDuration)
            {
                elapsedTime += Time.deltaTime;

                fadeScreen.color = Color.Lerp(endColor, startColor, elapsedTime / FadeDuration);

                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
            
            introText.text = "Based on a True Story";
            elapsedTime = 0f;
            
            while (elapsedTime < FadeDuration)
            {
                elapsedTime += Time.deltaTime;

                fadeScreen.color = Color.Lerp(startColor, endColor, elapsedTime / FadeDuration);

                yield return null;
            }

            yield return new WaitForSeconds(1.5f);
            
            elapsedTime = 0f;
            while (elapsedTime < FadeDuration)
            {
                elapsedTime += Time.deltaTime;

                fadeScreen.color = Color.Lerp(endColor, startColor, elapsedTime / FadeDuration);

                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(1);

        }
    }
}

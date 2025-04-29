using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DuckFriend.UI
{
    /// <summary>
    /// Manages the display of floating dialogue messages near the duck.
    /// </summary>
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private Text dialogueText;
        [SerializeField] private float fadeOutDuration = 0.5f;

        /// <summary>
        /// Displays a floating message for a specified duration.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="displayDuration">Time in seconds to display the message before fading out.</param>
        public void ShowMessage(string message, float displayDuration)
        {
            StopAllCoroutines();
            dialogueText.text = message;
            gameObject.SetActive(true);
            CanvasGroup cg = GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = 1f;
            }
            StartCoroutine(DisplayRoutine(displayDuration));
        }

        private IEnumerator DisplayRoutine(float displayDuration)
        {
            yield return new WaitForSeconds(displayDuration);
            CanvasGroup cg = GetComponent<CanvasGroup>();
            if (cg != null)
            {
                float elapsed = 0f;
                float startAlpha = cg.alpha;
                while (elapsed < fadeOutDuration)
                {
                    elapsed += Time.deltaTime;
                    cg.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeOutDuration);
                    yield return null;
                }
            }
            gameObject.SetActive(false);
        }
    }
} 
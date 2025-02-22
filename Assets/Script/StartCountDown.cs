// StartCountdown.cs
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public Image overlayPanel;
    public float countdownDuration = 1f; // Duration for each number
    public Timer gameTimer; // Reference to the Timer script

    private void Start()
    {
        // Disable the game timer until countdown is complete
        if (gameTimer != null)
        {
            gameTimer.enabled = false;
        }

        // Make sure overlay is visible and black/gray
        overlayPanel.color = new Color(0.2f, 0.2f, 0.2f, 0.9f);

        // Start the countdown
        StartCoroutine(BeginCountdown());
    }

    private System.Collections.IEnumerator BeginCountdown()
    {
        // Wait for a brief moment before starting
        yield return new WaitForSeconds(0.5f);

        // Countdown from 3 to 1
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();

            // Animate the text scale
            LeanTween.scale(countdownText.gameObject, Vector3.one * 1.5f, countdownDuration * 0.5f)
                .setEasePunch();

            yield return new WaitForSeconds(countdownDuration);
        }

        // Show "GO!" text
        countdownText.text = "GO!";
        LeanTween.scale(countdownText.gameObject, Vector3.one * 2f, 0.5f)
            .setEasePunch();

        // Fade out the overlay
        LeanTween.alpha(overlayPanel.rectTransform, 0f, 0.5f)
            .setOnComplete(() => {
                // Disable the countdown objects
                overlayPanel.gameObject.SetActive(false);
                countdownText.gameObject.SetActive(false);

                // Start the game timer
                if (gameTimer != null)
                {
                    gameTimer.enabled = true;
                    gameTimer.StartTimer();
                }
            });
    }
}
// GameOverManager.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public Button restartButton;
    public TextMeshProUGUI gameOverText;
    public string mainSceneName = "Main";

    private void Start()
    {
        // Set up restart button listener
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        // Animate the game over text
        if (gameOverText != null)
        {
            LeanTween.scale(gameOverText.gameObject, Vector3.one * 1.2f, 1f)
                .setEase(LeanTweenType.easeOutElastic)
                .setLoopPingPong();
        }
    }

    public void RestartGame()
    {
        StartCoroutine(TransitionToMainScene());
    }

    private System.Collections.IEnumerator TransitionToMainScene()
    {
        // Fade out effect
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            LeanTween.alphaCanvas(canvasGroup, 0f, 0.5f);
        }

        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    private void OnDestroy()
    {
        if (restartButton != null)
        {
            restartButton.onClick.RemoveListener(RestartGame);
        }
    }
}
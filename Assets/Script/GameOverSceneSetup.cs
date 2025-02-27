using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverSceneSetup : MonoBehaviour
{
    void Awake()
    {
        SetupGameOverScene();
    }

    void SetupGameOverScene()
    {
        // Get winning team from ScoreManager
        int winningTeam = ScoreManager.Instance.GetWinningTeam();
        Color teamColor = GetTeamColor(winningTeam);

        // Create Canvas
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();
        canvasObj.AddComponent<CanvasGroup>();

        // Background Panel
        GameObject bgPanel = CreatePanel("BackgroundPanel", canvasObj);
        Image bgImage = bgPanel.GetComponent<Image>();
        bgImage.color = new Color(.1f, .1f, .1f, .7f);

        // Game Over Text
        GameObject gameOverTextObj = new GameObject("GameOverText");
        gameOverTextObj.transform.SetParent(canvasObj.transform, false);
        TextMeshProUGUI gameOverText = gameOverTextObj.AddComponent<TextMeshProUGUI>();
        gameOverText.text = "GAME OVER";
        gameOverText.fontSize = 72;
        gameOverText.alignment = TextAlignmentOptions.Center;
        gameOverText.color = new Color(231f / 255f, 195f / 255f, 123f / 255f, 1f);
        RectTransform gameOverRect = gameOverText.GetComponent<RectTransform>();
        gameOverRect.anchoredPosition = new Vector2(0, 250);
        gameOverRect.sizeDelta = new Vector2(800, 100);

        // Winner Text
        GameObject winnerTextObj = new GameObject("WinnerText");
        winnerTextObj.transform.SetParent(canvasObj.transform, false);
        TextMeshProUGUI winnerText = winnerTextObj.AddComponent<TextMeshProUGUI>();
        winnerText.text = $"TEAM {winningTeam + 1} WINS!";
        winnerText.fontSize = 48;
        winnerText.alignment = TextAlignmentOptions.Center;
        winnerText.color = teamColor;
        RectTransform winnerRect = winnerText.GetComponent<RectTransform>();
        winnerRect.anchoredPosition = new Vector2(0, 0);
        winnerRect.sizeDelta = new Vector2(800, 100);

        // Use the actual score texts from ScoreManager
        GameObject scoreTextObj = new GameObject("ScoreText");
        scoreTextObj.transform.SetParent(canvasObj.transform, false);
        TextMeshProUGUI scoreText = scoreTextObj.AddComponent<TextMeshProUGUI>();
        scoreText.text = CreateFinalScoreText();
        scoreText.fontSize = 36;
        scoreText.alignment = TextAlignmentOptions.Center;
        scoreText.richText = true;
        RectTransform scoreRect = scoreText.GetComponent<RectTransform>();
        scoreRect.anchoredPosition = new Vector2(0, -100);
        scoreRect.sizeDelta = new Vector2(800, 100);

        // Restart Button
        GameObject restartButton = CreateButton("RestartButton", canvasObj, "RESTART", new Vector2(10, -250));

        // Style the restart button
        Image buttonImage = restartButton.GetComponent<Image>();
        buttonImage.color = new Color(101f / 255f, 150f / 255f, 148f / 255f, 1f);

        Button button = restartButton.GetComponent<Button>();
        button.onClick.AddListener(() => {
            // Simply reload the main scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        });
        ColorBlock colors = button.colors;
        colors.highlightedColor = new Color(0.4f, 0.4f, 0.4f);
        colors.pressedColor = new Color(0.2f, 0.2f, 0.2f);
        button.colors = colors;

        // Add GameOverManager
        GameOverManager manager = canvasObj.AddComponent<GameOverManager>();
        manager.restartButton = restartButton.GetComponent<Button>();
        manager.gameOverText = gameOverText;
    }
    private string CreateFinalScoreText()
    {
        string[] scoreTexts = new string[3];
        for (int i = 0; i < 3; i++)
        {
            string score = ScoreManager.Instance.teamScoreTexts[i].text;
            Color teamColor = GetTeamColor(i);
            string hexColor = ColorUtility.ToHtmlStringRGB(teamColor);
            scoreTexts[i] = $"<color=#{hexColor}>{score}</color>";
        }

        return string.Join("   ", scoreTexts);
    }


    private GameObject CreatePanel(string name, GameObject parent)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent.transform, false);
        Image image = panel.AddComponent<Image>();
        RectTransform rect = panel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        return panel;
    }

    private GameObject CreateButton(string name, GameObject parent, string text, Vector2 position)
    {
        GameObject buttonObj = new GameObject(name);
        buttonObj.transform.SetParent(parent.transform, false);

        Button button = buttonObj.AddComponent<Button>();
        Image buttonImage = buttonObj.AddComponent<Image>();

        RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();
        buttonRect.anchoredPosition = position;
        buttonRect.sizeDelta = new Vector2(200, 60);

        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform, false);
        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = 30;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = new Color(231f / 255f, 195f / 255f, 123f / 255f, 1f);

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;

        return buttonObj;
    }

    private Color GetTeamColor(int teamNumber)
    {
        switch (teamNumber)
        {
            case 0:
                return new Color(231f / 255f, 150f / 255f, 99f / 255f, 1f); // Red for team 1
            case 1:
                return new Color(231f / 255f, 199f / 255f, 130f / 255f, 1f); // Blue for team 2
            case 2:
                return new Color(171f / 255f, 180f / 255f, 127f / 255f, 1f);  // Green for team 3
            default:
                return Color.white;
        }
    }
}
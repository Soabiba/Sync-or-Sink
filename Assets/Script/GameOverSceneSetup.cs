// GameOverSceneSetup.cs
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
        bgImage.color = new Color(0.1f, 0.1f, 0.1f, 1f);

        // Game Over Text
        GameObject gameOverTextObj = new GameObject("GameOverText");
        gameOverTextObj.transform.SetParent(canvasObj.transform, false);
        TextMeshProUGUI gameOverText = gameOverTextObj.AddComponent<TextMeshProUGUI>();
        gameOverText.text = "GAME OVER";
        gameOverText.fontSize = 72;
        gameOverText.alignment = TextAlignmentOptions.Center;
        gameOverText.color = Color.white;
        RectTransform gameOverRect = gameOverText.GetComponent<RectTransform>();
        gameOverRect.anchoredPosition = new Vector2(0, 100);
        gameOverRect.sizeDelta = new Vector2(600, 100);

        // Restart Button
        GameObject restartButton = CreateButton("RestartButton", canvasObj, "RESTART", new Vector2(0, -50));

        // Style the restart button
        Image buttonImage = restartButton.GetComponent<Image>();
        buttonImage.color = new Color(0.3f, 0.3f, 0.3f);

        // Add hover effect to button
        Button button = restartButton.GetComponent<Button>();
        ColorBlock colors = button.colors;
        colors.highlightedColor = new Color(0.4f, 0.4f, 0.4f);
        colors.pressedColor = new Color(0.2f, 0.2f, 0.2f);
        button.colors = colors;

        // Add GameOverManager
        GameOverManager manager = canvasObj.AddComponent<GameOverManager>();
        manager.restartButton = restartButton.GetComponent<Button>();
        manager.gameOverText = gameOverText;
    }

    GameObject CreatePanel(string name, GameObject parent)
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

    GameObject CreateButton(string name, GameObject parent, string text, Vector2 position)
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
        tmp.color = Color.white;

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;

        return buttonObj;
    }
}
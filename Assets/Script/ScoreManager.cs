// ScoreManager.cs
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI[] teamScoreTexts;
    private int[] teamScores;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        teamScores = new int[3]; // 3 teams
    }

    public void AddPoint(int teamNumber)
    {
        teamScores[teamNumber]++;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        for (int i = 0; i < teamScoreTexts.Length; i++)
        {
            teamScoreTexts[i].text = $"Team {i + 1}: {teamScores[i]}";
        }
    }

    public int GetWinningTeam()
    {
        int highestScore = -1;
        int winningTeam = -1;

        for (int i = 0; i < teamScores.Length; i++)
        {
            if (teamScores[i] > highestScore)
            {
                highestScore = teamScores[i];
                winningTeam = i;
            }
        }

        return winningTeam;
    }
}

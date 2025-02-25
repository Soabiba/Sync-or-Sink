// GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playersContainer;
    public float playerSpacing = 2f;
    public float playerBaseY = -4f; // Bottom of the screen
    public static bool IsPaused = true;

    private KeyCode[] playerKeys = new KeyCode[]
    {
        KeyCode.Q, KeyCode.W,    // Team 1
        KeyCode.E, KeyCode.R,    // Team 2
        KeyCode.T, KeyCode.Y     // Team 3
    };

    private void Start()
    {
        SetupPlayers();
    }

    private void SetupPlayers()
    {
        float startX = -playerSpacing * 2.5f; // Center players

        for (int i = 0; i < 6; i++)
        {
            Vector3 position = new Vector3(startX + (i * playerSpacing), playerBaseY, 0);
            GameObject playerObj = Instantiate(playerPrefab, position, Quaternion.identity, playersContainer);

            Player player = playerObj.GetComponent<Player>();
            player.playerNumber = i;
            player.teamNumber = i / 2; // Assigns 0,0,1,1,2,2
            player.throwKey = playerKeys[i];

            // Set player color based on team
            SpriteRenderer sprite = playerObj.GetComponent<SpriteRenderer>();
            switch (i / 2)
            {
                case 0: sprite.color = Color.red; break;    // Team 1
                case 1: sprite.color = Color.blue; break;   // Team 2
                case 2: sprite.color = Color.green; break;  // Team 3
            }
        }
    }
}


// GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject playerPrefab;
    public Transform playersContainer;
    public float playerSpacing = 2f;
    private Transform ringContainer;
    public static bool IsPaused = true;

    [Header("Character Sprites")]
    public Sprite[] teamOneCharacters;
    public Sprite[] teamTwoCharacters;
    public Sprite[] teamThreeCharacters;

    private KeyCode[] playerKeys = new KeyCode[]
    {
        KeyCode.Q, KeyCode.W,    // Team 1
        KeyCode.E, KeyCode.R,    // Team 2
        KeyCode.T, KeyCode.Y     // Team 3
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CreateContainers();
        SetupPlayers();
    }

    private void CreateContainers()
    {
        if (GameObject.Find("RingContainer") == null)
        {
            GameObject ringContainerObj = new GameObject("RingContainer");
            ringContainer = ringContainerObj.transform;
        }
        else
        {
            ringContainer = GameObject.Find("RingContainer").transform;
        }
    }

    private void SetupPlayers()
    {
        float startX = -playerSpacing * 2.5f;

        for (int i = 0; i < 6; i++)
        {
            Vector3 position = new Vector3(startX + (i * playerSpacing), -4f, 0);
            GameObject playerObj = Instantiate(playerPrefab, position, Quaternion.identity, playersContainer);
            
            Player player = playerObj.GetComponent<Player>();
            SpriteRenderer spriteRenderer = playerObj.GetComponent<SpriteRenderer>();
            
            player.playerNumber = i;
            player.teamNumber = i / 2;
            player.throwKey = playerKeys[i];
            player.ringContainer = ringContainer;

            // Set character sprite based on team
            switch (i / 2) // teamNumber
            {
                case 0: // Team 1
                    spriteRenderer.sprite = teamOneCharacters[i % 2];
                    break;
                case 1: // Team 2
                    spriteRenderer.sprite = teamTwoCharacters[i % 2];
                    break;
                case 2: // Team 3
                    spriteRenderer.sprite = teamThreeCharacters[i % 2];
                    break;
            }

            // Adjust sprite size if needed
            AdjustSpriteSize(playerObj);
        }
    }

    public void ResetGame()
    {
        // Clear existing players
        if (playersContainer != null)
        {
            foreach (Transform child in playersContainer)
            {
                Destroy(child.gameObject);
            }
        }

        // Reset any other game state
        IsPaused = true;

        // Re-setup players
        CreateContainers();
        SetupPlayers();
    }

    private void AdjustSpriteSize(GameObject player)
    {
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        BoxCollider2D collider = player.GetComponent<BoxCollider2D>();
        
        // Adjust these values based on your desired character size
        float targetHeight = 1f; // desired height in Unity units
        float scale = targetHeight / spriteRenderer.sprite.bounds.size.y;
        
        player.transform.localScale = new Vector3(scale, scale, 1);
        
        // Update collider to match sprite
        if (collider != null)
        {
            collider.size = spriteRenderer.sprite.bounds.size;
        }
    }
}
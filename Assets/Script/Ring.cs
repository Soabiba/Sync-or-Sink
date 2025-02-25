using UnityEngine;

public class Ring : MonoBehaviour
{
    public int teamNumber;
    private bool hasScored = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetTeamColor();
        Destroy(gameObject, 3f); // Destroy after 3 seconds if no collision
    }

    void SetTeamColor()
    {
        switch (teamNumber)
        {
            case 0: // Team 1
                spriteRenderer.color = Color.red;
                break;
            case 1: // Team 2
                spriteRenderer.color = Color.blue;
                break;
            case 2: // Team 3
                spriteRenderer.color = Color.green;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasScored) return;

        if (other.CompareTag("Monster"))
        {
            hasScored = true;
            ScoreManager.Instance.AddPoint(teamNumber);
            Destroy(gameObject);
        }
    }
}


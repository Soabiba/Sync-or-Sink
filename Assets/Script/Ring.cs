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

    void Update()
    {
        if (GameManager.IsPaused) return;
    }

    Color HexToColor(string hex)
    {
        // Remove # if present
        if (hex.StartsWith("#"))
        {
            hex = hex.Substring(1);
        }

        // Parse the hex string
        if (hex.Length == 6)
        {
            float r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
            float g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
            float b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f;

            return new Color(r, g, b);
        }

        // Handle RGBA hex codes (8 characters)
        else if (hex.Length == 8)
        {
            float r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
            float g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
            float b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
            float a = int.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber) / 255f;

            return new Color(r, g, b, a);
        }

        Debug.LogError("Invalid hex color format: " + hex);
        return Color.white; // Default fallback
    }

    void SetTeamColor()
    {
        switch (teamNumber)
        {
            case 0: // Team 1
                spriteRenderer.color = HexToColor("#dc8755");
                break;
            case 1: // Team 2
                spriteRenderer.color = HexToColor("#e7c782");
                break;
            case 2: // Team 3
                spriteRenderer.color = HexToColor("#abb47f");
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


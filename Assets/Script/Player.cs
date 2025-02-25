// Player.cs
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNumber;
    public int teamNumber;
    public KeyCode throwKey;
    public GameObject ringPrefab;
    public float throwForce = 1f;

    private void Update()
    {
        if (GameManager.IsPaused) return;

        if (Input.GetKeyDown(throwKey))
        {
            ThrowRing();
        }
    }

    private void ThrowRing()
    {
        // Spawn ring slightly above player
        Vector3 spawnPosition = transform.position + Vector3.up * 0.5f;
        GameObject ring = Instantiate(ringPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D ringRb = ring.GetComponent<Rigidbody2D>();
        Ring ringScript = ring.GetComponent<Ring>();

        // Throw straight up
        ringRb.AddForce(Vector2.up * throwForce, ForceMode2D.Impulse);
        ringScript.teamNumber = teamNumber;
    }
}


using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerNumber;
    public int teamNumber;
    public KeyCode throwKey;
    public GameObject ringPrefab;
    public float throwForce = 5f;
    public Transform ringContainer;

    [Header("Cooldown Settings")]
    public float cooldownTime = 2f;
    public GameObject cooldownBarPrefab;

    private float currentCooldown = 0f;
    private bool canThrow = true;
    private Image cooldownFillImage;
    private GameObject cooldownBar;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetupCooldownBar();
    }

    private void SetupCooldownBar()
    {
        // Create cooldown bar above player
        cooldownBar = Instantiate(cooldownBarPrefab, transform);
        cooldownBar.transform.localPosition = new Vector3(0, 1.2f, 0); // Position above player
        cooldownFillImage = cooldownBar.transform.Find("Fill").GetComponent<Image>();

        // Set the color based on team
        if (teamNumber == 0)
            cooldownFillImage.color = Color.red;
        else if (teamNumber == 1)
            cooldownFillImage.color = Color.blue;

        cooldownFillImage.fillAmount = 1; // Start full
    }

    private void Update()
    {
        if (GameManager.IsPaused) return;
        // Update cooldown
        if (!canThrow)
        {
            currentCooldown -= Time.deltaTime;
            cooldownFillImage.fillAmount = currentCooldown / cooldownTime;

            if (currentCooldown <= 0)
            {
                canThrow = true;
                cooldownFillImage.fillAmount = 1;
            }
        }

        // Handle throwing
        if (canThrow && Input.GetKeyDown(throwKey))
        {
            ThrowRing();
            StartCooldown();
        }

        // Keep cooldown bar facing camera
        if (cooldownBar != null)
        {
            cooldownBar.transform.rotation = Quaternion.identity;
        }
    }

    private void StartCooldown()
    {
        canThrow = false;
        currentCooldown = cooldownTime;
        cooldownFillImage.fillAmount = 0;
    }

    private void ThrowRing()
    {
        Vector3 spawnPosition = transform.position + Vector3.up * 0.5f;
        GameObject ring = Instantiate(ringPrefab, spawnPosition, Quaternion.identity, ringContainer);

        Rigidbody2D ringRb = ring.GetComponent<Rigidbody2D>();
        Ring ringScript = ring.GetComponent<Ring>();

        ringRb.linearVelocity = Vector2.zero;
        ringRb.AddForce(Vector2.up * throwForce, ForceMode2D.Impulse);
        ringScript.teamNumber = teamNumber;
    }
}
// MonsterMovement.cs
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float startSpeed = 2f;
    public float maxSpeed = 8f;
    public float leftBound = -8f;
    public float rightBound = 8f;

    private float currentSpeed;
    private bool movingRight = true;
    private Timer timer;
    private float fixedYPosition;

    private void Start()
    {
        currentSpeed = startSpeed;
        timer = Object.FindAnyObjectByType<Timer>();
        fixedYPosition = transform.position.y; 
    }

    private void Update()
    {
        if (timer != null)
        {
            UpdateSpeed();
            MoveMonster();
            LockYPosition();
        }

        if (GameManager.IsPaused) return;
    }

    private void UpdateSpeed()
    {
        currentSpeed = Mathf.Lerp(startSpeed, maxSpeed, timer.TimeProgress);
        if (GameManager.IsPaused) return;
    }

    private void MoveMonster()
    {
        float newXPosition = transform.position.x;

        if (movingRight)
        {
            newXPosition += currentSpeed * Time.deltaTime;
            if (newXPosition >= rightBound)
            {
                movingRight = false;
                newXPosition = rightBound;
                // Optional: Flip monster sprite if needed
                // transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            newXPosition -= currentSpeed * Time.deltaTime;
            if (newXPosition <= leftBound)
            {
                movingRight = true;
                newXPosition = leftBound;
                // Optional: Flip monster sprite if needed
                // transform.localScale = new Vector3(1, 1, 1);
            }
        }

        transform.position = new Vector3(newXPosition, fixedYPosition, transform.position.z);
    }

    private void LockYPosition()
    {
        if (transform.position.y != fixedYPosition)
        {
            Vector3 position = transform.position;
            position.y = fixedYPosition;
            transform.position = position;
        }
    }
}
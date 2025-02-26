using UnityEngine;

public class CooldownBar : MonoBehaviour
{
    private void Start()
    {
        // Make sure the cooldown bar faces the camera
        transform.rotation = Quaternion.identity;
    }

    private void LateUpdate()
    {
        // Keep the bar facing the camera
        transform.rotation = Quaternion.identity;
    }
}
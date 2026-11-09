using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            // Наносим огромный урон, чтобы сработала смерть
            playerHealth.TakeDamage(9999f);
        }
    }
}
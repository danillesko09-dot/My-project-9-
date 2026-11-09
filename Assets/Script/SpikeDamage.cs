using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 20f; // Сколько урона наносит

    // Срабатывает, когда шарик заходит в зону коллайдера шипа
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Пытаемся найти скрипт здоровья на объекте, в который врезались
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
            
            // Если хочешь, чтобы шип откидывал шарик, можно добавить импульс:
            // Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            // if (rb != null) rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }
}

using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healAmount = 30f; // Сколько здоровья восстанавливает

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            // Лечим игрока
            playerHealth.Heal(healAmount);
            
            // Хилка "исчезает" (выключаем объект, чтобы его потом можно было включить при респавне)
            gameObject.SetActive(false); 
        }
    }
}
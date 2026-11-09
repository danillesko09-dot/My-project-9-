using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Здоровье")]
    [SerializeField] private float maxHealth = 100f;
    public float currentHealth; 

    [Header("Ссылки")]
    [SerializeField] private PlayerControl playerControl; // Ссылка на управление
    private PlayerRespawn respawnScript;

    private void Awake()
    {
        respawnScript = GetComponent<PlayerRespawn>();
        if (playerControl == null) playerControl = GetComponent<PlayerControl>();

        if (respawnScript == null)
        {
            Debug.LogError("ОШИБКА: На шарике нет скрипта PlayerRespawn! Смерть не сработает.");
        }

        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log("Получен урон: " + damage + ". Осталось здоровья: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        if (currentHealth <= 0) return;

        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        Debug.Log("Вылечились! Здоровье: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log("<color=red>ИГРОК УМЕР! Вызываем респавн...</color>");
        
        // Отключаем управление
        if (playerControl != null) 
            playerControl.enabled = false;

        // Запускаем респавн
        if (respawnScript != null)
        {
            respawnScript.Respawn();
        }
    }
}
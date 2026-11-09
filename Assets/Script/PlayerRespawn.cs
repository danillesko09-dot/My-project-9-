using UnityEngine;
using System.Collections; // Обязательно должно быть в самом верху!

public class PlayerRespawn : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private ScreenFader screenFader; 
    [SerializeField] private float deathDelay = 0.5f; 

    private Vector3 spawnPosition;
    private Rigidbody2D rb;
    private PlayerHealth healthScript;
    private PlayerControl playerControl;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthScript = GetComponent<PlayerHealth>();
        playerControl = GetComponent<PlayerControl>();
        
        // Запоминаем стартовую позицию
        spawnPosition = transform.position;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        // 1. Задержка перед затемнением
        yield return new WaitForSeconds(deathDelay);

        // 2. Экран темнеет
        if (screenFader != null)
        {
            screenFader.FadeToBlack();
            yield return new WaitForSeconds(0.5f); // Ждем, пока потемнеет
        }

        // 3. Телепорт и сброс физики
        transform.position = spawnPosition;
        if (rb != null) 
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // 4. Восстановление здоровья
        if (healthScript != null) 
            healthScript.currentHealth = 100f; // Возвращаем полное здоровье

        // 5. Возрождение хилок
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("HealthPickup");
        foreach (GameObject pickup in pickups) 
        {
            pickup.SetActive(true);
        }

        // 6. Включаем управление обратно
        if (playerControl != null) 
            playerControl.enabled = true;

        // 7. Экран светлеет
        if (screenFader != null)
        {
            screenFader.FadeFromBlack();
        }
        
        Debug.Log("<color=green>Игрок успешно зареспавнился!</color>");
    }
}

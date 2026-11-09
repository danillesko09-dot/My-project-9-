using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    [Header("Настройки перехода")]
    [Tooltip("Напиши точное имя сцены, куда нужно телепортироваться")]
    [SerializeField] private string sceneName; 

    // Срабатывает, когда шарик заходит в зону (нужен коллайдер с галочкой Is Trigger)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что в зону зашел именно игрок (у которого есть скрипт PlayerControl)
        if (collision.GetComponent<PlayerControl>() != null)
        {
            // Защита от случайного имени с пустыми пробелами
            if (!string.IsNullOrEmpty(sceneName))
            {
                Debug.Log("Телепортация в сцену: " + sceneName);
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("ОШИБКА: В скрипте телепорта не указано имя сцены!");
            }
        }
    }
}
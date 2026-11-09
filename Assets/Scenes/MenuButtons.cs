using UnityEngine;
using UnityEngine.SceneManagement; // Обязательно для работы со сценами

public class MenuButtons : MonoBehaviour
{
    // ==========================================
    // 1. ЗАГРУЗКА СЛЕДУЮЩЕЙ СЦЕНЫ (по индексу)
    // ==========================================
    public void LoadNextScene()
    {
        // Узнаем индекс текущей сцены и прибавляем 1
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Проверяем, существует ли вообще следующая сцена в Build Settings
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Следующей сцены нет! Ты прошел игру или забыл добавить сцену в Build Settings.");
            // Можно вернуть в главное меню (индекс 0)
            // SceneManager.LoadScene(0); 
        }
    }

    // ==========================================
    // 2. ЗАГРУЗКА ПО ИМЕНИ (если нужно конкретную)
    // ==========================================
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ==========================================
    // 3. ВЫХОД ИЗ ИГРЫ
    // ==========================================
    public void QuitGame()
    {
        Debug.Log("Игра закрывается...");
        Application.Quit(); 
    }
}
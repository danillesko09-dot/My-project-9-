using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Нужно для корутин (задержек)

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.5f; // За сколько секунд экран темнеет/светлеет

    private void Awake()
    {
        if (fadeImage == null)
            fadeImage = GetComponent<Image>();

        // В начале игры экран полностью прозрачный
        SetAlpha(0f);
    }

    // Метод для затемнения (вызывается при смерти)
    public void FadeToBlack()
    {
        StopAllCoroutines(); // Останавливаем старые эффекты, если были
        StartCoroutine(FadeRoutine(1f)); // 1f = полностью черный
    }

    // Метод для возвращения из темноты (вызывается при возрождении)
    public void FadeFromBlack()
    {
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(0f)); // 0f = полностью прозрачный
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            // Плавно вычисляем прозрачность от старой к целевой
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            SetAlpha(newAlpha);
            yield return null; // Ждем следующий кадр
        }

        SetAlpha(targetAlpha); // На всякий случай ставим точное значение в конце
    }

    private void SetAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}
using UnityEngine;
using UnityEngine.UI; // Обязательно для работы с картинками UI

public class HealthBar : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private PlayerHealth playerHealth; // Сюда перетащишь шарик
    [SerializeField] private Image fillImage; // Сюда перетащишь саму полоску (HealthBarFill)

    [Header("Настройки")]
    [SerializeField] private bool smoothTransition = true; // Плавно ли убавляется
    [SerializeField] private float smoothSpeed = 5f; // Скорость плавности

    private float targetFillAmount = 1f;
    private float maxHealth;

    private void Start()
    {
        // Если полоска не назначена в инспекторе, берем компонент с этого же объекта
        if (fillImage == null) 
            fillImage = GetComponent<Image>();

        // Ищем игрока на сцене, если забыли перетащить
        if (playerHealth == null)
            playerHealth = FindFirstObjectByType<PlayerHealth>();

        if (playerHealth != null)
        {
            // Запоминаем максимальное здоровье (через рефлексию, так как оно private в PlayerHealth)
            // *Если в PlayerHealth maxHealth сделан public, можно просто взять playerHealth.maxHealth
            maxHealth = 100f; // Поставь то же значение, что и в PlayerHealth!
            
            // Сразу выставляем полоску на старте
            fillImage.fillAmount = 1f;
        }
        else
        {
            Debug.LogError("ХП-бар: Не удалось найти игрока (PlayerHealth)!");
        }
    }

    private void Update()
    {
        if (playerHealth == null || fillImage == null) return;

        // Считаем, сколько процентов здоровья осталось (от 0 до 1)
        // Используем переменную currentHealth из твоего скрипта здоровья
        targetFillAmount = playerHealth.currentHealth / maxHealth;

        // Если плавность выключена — просто ставим значение
        if (!smoothTransition)
        {
            fillImage.fillAmount = targetFillAmount;
        }
        else
        {
            // Плавно интерполируем текущее значение к целевому
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFillAmount, smoothSpeed * Time.deltaTime);
        }
    }
}
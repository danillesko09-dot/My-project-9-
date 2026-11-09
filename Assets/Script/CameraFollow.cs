using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);

    // Скрытая переменная для хранения сдвига камеры (для параллакса)
    [HideInInspector] public Vector3 deltaMovement; 
    
    private Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    // ВАЖНО: Используем FixedUpdate, так как шарик двигается в FixedUpdate (физика)
    void FixedUpdate()
    {
        if (target == null) return;

        // Жестко привязываем камеру к шарику. Никаких SmoothDamp.
        transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, offset.z);

        // Записываем сдвиг для фона
        deltaMovement = transform.position - previousPosition;
        previousPosition = transform.position;
    }
}
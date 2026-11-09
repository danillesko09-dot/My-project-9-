using UnityEngine;

public class ParalaxBg : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float ParalaxSettings = 0.1f; 
    [SerializeField] private bool DisableVerticalParalax = true; 
    [SerializeField] private CameraFollow cameraFollow; 

    void FixedUpdate() // Меняем LateUpdate на FixedUpdate!
    {
        if (cameraFollow == null) return;

        Vector3 delta = cameraFollow.deltaMovement;

        if (DisableVerticalParalax)
            delta.y = 0;

        transform.position += delta * ParalaxSettings;
    }
}
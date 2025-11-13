using UnityEngine;

public class AuraRotator : MonoBehaviour
{
    [Header("Configuración de rotación")]
    public float rotationSpeed = 30f; // grados por segundo

    [Header("Escalado de aparición")]
    public float appearDuration = 0.8f;
    private float appearTimer;
    private Vector3 initialScale;
    private bool isAppearing;

    void Start()
    {
        initialScale = transform.localScale;
        transform.localScale = Vector3.zero;
        isAppearing = true;
        appearTimer = 0;
    }

    void Update()
    {
        // Rotación continua
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        // Animación de aparición suave
        if (isAppearing)
        {
            appearTimer += Time.deltaTime / appearDuration;
            float t = Mathf.SmoothStep(0, 1, appearTimer);
            transform.localScale = Vector3.Lerp(Vector3.zero, initialScale, t);
            if (t >= 1f)
                isAppearing = false;
        }
    }
}

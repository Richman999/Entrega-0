using UnityEngine;

public class MaterialSwitcher : MonoBehaviour
{
    [Header("Referencias")]
    public Renderer targetRenderer; // Asigna el Renderer del objeto
    public Material originalMaterial; // Material original del objeto
    public Material shaderMaterial;   // Material con el shader personalizado

    private bool initialized = false;

    void Start()
    {
        // Si no se asignó manualmente, intenta detectar el Renderer
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        initialized = true;
    }

    // Cambia al material del shader
    public void SetShaderMaterial()
    {
        if (!initialized) Start();

        if (targetRenderer != null && shaderMaterial != null)
        {
            targetRenderer.sharedMaterial = shaderMaterial;
        }
    }

    // Restaura el material original
    public void SetOriginalMaterial()
    {
        if (!initialized) Start();

        if (targetRenderer != null && originalMaterial != null)
        {
            targetRenderer.sharedMaterial = originalMaterial;
        }
    }
}

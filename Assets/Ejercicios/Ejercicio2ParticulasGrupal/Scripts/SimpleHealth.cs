using UnityEngine;


public class SimpleHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;


    void Start() { currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); }


    public void ApplyDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        // aqu� puedes lanzar animaciones o eventos
    }


    public void ApplyHeal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
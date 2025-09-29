using System.Collections;
using UnityEngine;

public class HealingVFXController : MonoBehaviour
{
    private ParticleSystem[] particleSystems;
    private Coroutine activeRoutine;

    void Awake()
    {
        // Cacheamos todos los sistemas hijos
        particleSystems = GetComponentsInChildren<ParticleSystem>();

        // Aseguramos que todos estén en Stop al inicio
        foreach (var ps in particleSystems)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (activeRoutine != null)
                StopCoroutine(activeRoutine);

            activeRoutine = StartCoroutine(PlayForSeconds(5f));
        }
    }

    private IEnumerator PlayForSeconds(float seconds)
    {
        // Play de todos los sistemas
        foreach (var ps in particleSystems)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting); // reinicio limpio pero sin borrar
            ps.Play(true);
        }

        // Esperamos X segundos
        yield return new WaitForSeconds(seconds);

        // Stop de todos los sistemas ? desaparecen naturalmente
        foreach (var ps in particleSystems)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        activeRoutine = null;
    }
}

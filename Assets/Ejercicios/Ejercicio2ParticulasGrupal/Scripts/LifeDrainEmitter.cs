using UnityEngine;

public class LifeDrainEmitter : MonoBehaviour
{
    [Header("Particles")]
    public ParticleSystem runePrefab; // prefab RuneDrain (ParticleSystem)
    public Transform target; // personaje A (donde llegan las runas)

    [Header("Tuning")]
    public float desiredTravelTime = 1.0f; // tiempo objetivo para que la partícula viaje
    public int burstCount = 20;
    public float destroyAfter = 3f;

    public void PlayDrain()
    {
        if (runePrefab == null || target == null) return;

        // instantiate at emitter position
        ParticleSystem ps = Instantiate(runePrefab, transform.position, Quaternion.identity);
        var main = ps.main;
        // ajusta startLifetime cercano a desiredTravelTime para consistencia
        main.startLifetime = new ParticleSystem.MinMaxCurve(desiredTravelTime * 0.9f, desiredTravelTime * 1.1f);

        // distancia / velocidad
        float distance = Vector3.Distance(transform.position, target.position);
        float speed = Mathf.Max(0.01f, distance / desiredTravelTime);
        Vector3 dir = (target.position - transform.position).normalized;

        // configurar velocity over lifetime (espacio world)
        var vel = ps.velocityOverLifetime;
        vel.enabled = true;
        vel.space = ParticleSystemSimulationSpace.World;
        vel.x = new ParticleSystem.MinMaxCurve(dir.x * speed);
        vel.y = new ParticleSystem.MinMaxCurve(dir.y * speed);
        vel.z = new ParticleSystem.MinMaxCurve(dir.z * speed);

        // configurar bursts
        var emission = ps.emission;
        emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0f, (short)burstCount) });

        // reproducir
        ps.transform.position = transform.position;
        ps.Play();

        // destruir pasado un tiempo
        Destroy(ps.gameObject, destroyAfter);
    }
}

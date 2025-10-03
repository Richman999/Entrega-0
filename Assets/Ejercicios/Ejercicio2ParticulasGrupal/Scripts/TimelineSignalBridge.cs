using UnityEngine;

public class TimelineSignalBridge : MonoBehaviour
{
    public LifeDrainEmitter emitter;

    // M�todo p�blico para llamar desde Signal Receiver en Timeline
    public void PlayDrainSignal()
    {
        if (emitter != null) emitter.PlayDrain();
    }
}

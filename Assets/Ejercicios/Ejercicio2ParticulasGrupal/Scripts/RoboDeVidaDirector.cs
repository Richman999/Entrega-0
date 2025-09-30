using UnityEngine;
using UnityEngine.Playables;

public class LifeDrainTimelineController : MonoBehaviour
{
    public PlayableDirector director;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (director != null)
            {
                director.time = 0; // Reinicia al inicio
                director.Play();
            }
        }
    }
}

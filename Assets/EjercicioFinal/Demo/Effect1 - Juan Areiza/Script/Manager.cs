using UnityEngine;
using UnityEngine.Playables;

public class AnimationUIController : MonoBehaviour
{
    public PlayableDirector director;

    public PlayableAsset anim1;
    public PlayableAsset anim2;
    public PlayableAsset anim3;

    public void PlayAnim1()
    {
        director.playableAsset = anim1;
        director.time = 0;
        director.Play();
    }

    public void PlayAnim2()
    {
        director.playableAsset = anim2;
        director.time = 0;
        director.Play();
    }

    public void PlayAnim3()
    {
        director.playableAsset = anim3;
        director.time = 0;
        director.Play();
    }
}
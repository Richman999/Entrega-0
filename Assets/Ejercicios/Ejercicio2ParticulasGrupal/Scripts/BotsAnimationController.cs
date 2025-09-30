using UnityEngine;

public class BotsAnimationController : MonoBehaviour
{
    public Animator Per_BController;
    public Animator Per_AController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Per_BController != null)
                Per_BController.SetTrigger("PlayAction");

            if (Per_AController != null)
                Per_AController.SetTrigger("PlayAction");
        }
    }
}

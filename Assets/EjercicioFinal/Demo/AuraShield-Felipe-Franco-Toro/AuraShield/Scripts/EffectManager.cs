using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    public GameObject[] effects; // arrastra AuraShield y los otros efectos
    public Button nextButton;
    public Button playPauseButton;

    private int currentIndex = 0;
    private bool isPlaying = true;

    void Start()
    {
        ShowEffect(0);
        nextButton.onClick.AddListener(NextEffect);
        playPauseButton.onClick.AddListener(TogglePlay);
    }

    void ShowEffect(int index)
    {
        for (int i = 0; i < effects.Length; i++)
            effects[i].SetActive(i == index);

        currentIndex = index;
    }

    void NextEffect()
    {
        int next = (currentIndex + 1) % effects.Length;
        ShowEffect(next);
    }

    void TogglePlay()
    {
        isPlaying = !isPlaying;
        effects[currentIndex].SetActive(isPlaying);
    }
}

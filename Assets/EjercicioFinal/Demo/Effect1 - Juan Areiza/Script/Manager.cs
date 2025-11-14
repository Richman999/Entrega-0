using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class AnimationUIController : MonoBehaviour
{
    [Header("Timelines")]
    public PlayableDirector[] directors;
    private int currentIndex = -1;     // Ningún timeline activo al inicio
    private PlayableDirector currentDirector = null;

    [Header("Camera Animator")]
    public Animator cameraAnimator;

    [Header("Character Material Override")]
    public Renderer characterRenderer;     // Renderer del personaje
    public Material startMaterial;         // Material con el que quieres arrancar la demo

    [Header("UI")]
    public Button playPauseButton;
    public Button effect1Button;
    public Button effect2Button;
    public Button effect3Button;
    public Text statusText;

    private bool isPaused = false;

    void Start()
    {
        // Aplicar material inicial SOLO al comienzo
        if (characterRenderer != null && startMaterial != null)
            characterRenderer.material = startMaterial;

        // Desactivar todos los timelines al inicio
        for (int i = 0; i < directors.Length; i++)
            directors[i].gameObject.SetActive(false);

        // La cámara debe iniciar en estado neutral (IdleEmpty)
        if (cameraAnimator != null)
            cameraAnimator.SetBool("ActiveVFX1", false);

        // Conectar botones UI
        playPauseButton.onClick.AddListener(TogglePlayPause);
        effect1Button.onClick.AddListener(() => SwitchEffect(0));
        effect2Button.onClick.AddListener(() => SwitchEffect(1));
        effect3Button.onClick.AddListener(() => SwitchEffect(2));

        UpdateStatusText();
    }

    void TogglePlayPause()
    {
        if (currentDirector == null)
            return; // No hay timeline activo aún

        if (isPaused)
        {
            currentDirector.Play();
            isPaused = false;
        }
        else
        {
            currentDirector.Pause();
            isPaused = true;
        }

        UpdateStatusText();
    }

    void SwitchEffect(int index)
    {
        // REINICIAR si es el mismo timeline
        if (index == currentIndex)
        {
            currentDirector.time = 0;
            currentDirector.Play();
            isPaused = false;

            if (cameraAnimator != null)
                cameraAnimator.SetBool("ActiveVFX1", index == 0);

            UpdateStatusText();
            return;
        }

        // SI ES OTRO TIMELINE:
        // Detener el actual si existe
        if (currentDirector != null)
        {
            currentDirector.Stop();
            currentDirector.gameObject.SetActive(false);
        }

        // Activar nuevo
        currentIndex = index;
        currentDirector = directors[currentIndex];
        currentDirector.gameObject.SetActive(true);

        // Reproducir desde el inicio
        currentDirector.time = 0;
        currentDirector.Play();
        isPaused = false;

        // ? Activar/desactivar animación de cámara según el Timeline
        if (cameraAnimator != null)
            cameraAnimator.SetBool("ActiveVFX1", index == 0);

        UpdateStatusText();
    }

    void UpdateStatusText()
    {
        if (statusText != null)
        {
            if (currentDirector == null)
            {
                statusText.text = "Bienvenid@ a nuestra demo :)";
                return;
            }

            string state = isPaused ? "Pausado" : "Reproduciendo";
            statusText.text = state + " — " + currentDirector.name;
        }
    }
}

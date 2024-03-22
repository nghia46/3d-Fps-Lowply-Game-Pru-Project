using UnityEngine;

public class HandleMusicSoundSlider : MonoBehaviour
{
    [SerializeField] private AudioSource[] MusicSources;
    private void Start() {
        foreach (var source in MusicSources)
        {
             GetComponent<UnityEngine.UI.Slider>().value = source.volume;
        }
    }
    private void Update() {
        foreach (var source in MusicSources)
        {
            source.volume = GetComponent<UnityEngine.UI.Slider>().value;
        }
    }
}

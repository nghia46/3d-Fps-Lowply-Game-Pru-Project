using UnityEngine;

public class HandleSFXSoundSlider : MonoBehaviour
{
    [SerializeField] private AudioSource[] SFXSources;
    private void Start() {
        foreach (var source in SFXSources)
        {
             GetComponent<UnityEngine.UI.Slider>().value = source.volume;
        }
    }
    private void Update() {
        foreach (var source in SFXSources)
        {
            source.volume = GetComponent<UnityEngine.UI.Slider>().value;
        }
    }
}

using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] sounds;
    private enum SoundType
    {
        Fire,
        Reload
    }
    private void Start()
    {
        EventManager.Instance.FireEvent += PlayFireSound;
        EventManager.Instance.ReloadingEvent += PlayReloadingSound;
    }

    private void PlayFireSound()
    {
        sounds[(int)SoundType.Fire].Play();
    }
    private void PlayReloadingSound()
    {
        sounds[(int)SoundType.Reload].Play();
    }
    private void OnDisable()
    {
        EventManager.Instance.ReloadingEvent -= PlayReloadingSound;
        EventManager.Instance.FireEvent -= PlayFireSound;
    }
}
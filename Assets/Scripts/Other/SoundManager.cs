using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] sounds;
    private enum SoundType
    {
        Fire,
        Reload,
        NeedReload,
    }
    private void Start()
    {
        EventManager.Instance.FireEvent += PlayFireSound;
        EventManager.Instance.NeedReloadingEvent += PlayNeedReloadSound;
        EventManager.Instance.ReloadEvent += PlayReloadSound;
    }

    private void PlayFireSound()
    {
        sounds[(int)SoundType.Fire].Play();
    }
    private void PlayNeedReloadSound()
    {
        sounds[(int)SoundType.NeedReload].Play();
    }
    private void PlayReloadSound()
    {
        sounds[(int)SoundType.Reload].Play();
    }
    private void OnDisable()
    {
        EventManager.Instance.NeedReloadingEvent -= PlayNeedReloadSound;
        EventManager.Instance.FireEvent -= PlayFireSound;
        EventManager.Instance.ReloadEvent -= PlayReloadSound;
    }
}
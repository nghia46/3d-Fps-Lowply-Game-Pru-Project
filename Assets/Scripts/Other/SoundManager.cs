using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] sounds;
    private enum SoundType
    {
        Fire
    }
    private void Start()
    {
        EventManager.Instance.FireEvent += PlayFireSound;
    }

    private void PlayFireSound()
    {
        sounds[(int)SoundType.Fire].Play();
    }

    private void OnDisable()
    {
        EventManager.Instance.FireEvent += PlayFireSound;

    }
}
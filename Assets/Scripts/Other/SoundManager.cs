using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] GunSounds;
    [SerializeField] private AudioSource[] CollectibleSounds;
    [SerializeField] private AudioSource[] EnemySounds;
    private enum GunSoundType
    {
        Fire,
        Reload,
        NeedReload,
    }
    private enum CollectibleoundType
    {
        Coin,
        Health,
        Armor,
    }
    private enum EnemySoundType
    {
        Hit,
        Die,
    }
    private void Start()
    {
        EventManager.Instance.FireEvent += PlayFireSound;
        EventManager.Instance.NeedReloadingEvent += PlayNeedReloadSound;
        EventManager.Instance.ReloadEvent += PlayReloadSound;
        EventManager.Instance.CollectibleEvent += PlayCollectibleSound;
        EventManager.Instance.EnemyAttackEvent += PlayEnemyAttackSound;
        EventManager.Instance.EnemyDieEvent += PlayEnemyDieSound;
    }
    private void PlayCollectibleSound(int id, int quantity)
    {
        switch(id)
        {
            case (int)CollectibleoundType.Coin:
                CollectibleSounds[(int)CollectibleoundType.Coin].Play();
                break;
            case (int)CollectibleoundType.Health:
                CollectibleSounds[(int)CollectibleoundType.Health].Play();
                break;
            case (int)CollectibleoundType.Armor:
                CollectibleSounds[(int)CollectibleoundType.Armor].Play();
                break;
        }
        
    }
    private void PlayEnemyDieSound()
    {
        EnemySounds[(int)EnemySoundType.Die].Play();
    }
    private void PlayEnemyAttackSound()
    {
        EnemySounds[(int)EnemySoundType.Hit].Play();
    }
    private void PlayFireSound()
    {
        GunSounds[(int)GunSoundType.Fire].Play();
    }
    private void PlayNeedReloadSound()
    {
        GunSounds[(int)GunSoundType.NeedReload].Play();
    }
    private void PlayReloadSound()
    {
        GunSounds[(int)GunSoundType.Reload].Play();
    }
    private void OnDisable()
    {
        EventManager.Instance.NeedReloadingEvent -= PlayNeedReloadSound;
        EventManager.Instance.FireEvent -= PlayFireSound;
        EventManager.Instance.ReloadEvent -= PlayReloadSound;
        EventManager.Instance.CollectibleEvent -= PlayCollectibleSound;
        EventManager.Instance.EnemyAttackEvent -= PlayEnemyAttackSound;
    }
}
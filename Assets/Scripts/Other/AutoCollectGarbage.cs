using UnityEngine;

public class AutoCollectGarbage : MonoBehaviour
{
    [SerializeField] private float timePerCollect;
    [SerializeField] private int entityCount;
    void Start()
    {
        InvokeRepeating("Collect", 0, timePerCollect);
    }
    void Collect()
    {
        entityCount = transform.childCount;
        if (entityCount <= 0) return;
        var firstGameObject = transform.GetChild(0).gameObject;
        Destroy(firstGameObject);
    }

}

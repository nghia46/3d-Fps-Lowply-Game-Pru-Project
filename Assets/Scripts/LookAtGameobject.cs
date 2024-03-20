using UnityEngine;

public class LookAtGameobject : MonoBehaviour
{
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
    }
}

using UnityEngine;

namespace Other
{
    public class FollowPosition : MonoBehaviour
    {
        [SerializeField]private GameObject PlayerPosition;
        void Update()
        {
            transform.position = PlayerPosition.transform.position;
        }  
    }
}
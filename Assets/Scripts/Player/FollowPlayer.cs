using UnityEngine;

namespace Player
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField]private GameObject PlayerPosition;
        void Update()
        {
            transform.position = PlayerPosition.transform.position;
        }
    }
}
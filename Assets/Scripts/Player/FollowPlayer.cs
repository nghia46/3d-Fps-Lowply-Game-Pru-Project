using UnityEngine;

namespace Player
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField]private GameObject PlayerObjectPos;
        void Update()
        {
            transform.position = PlayerObjectPos.transform.position;
        }
    }
}
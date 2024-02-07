using Unity.Collections;
using UnityEngine;
[CreateAssetMenu(menuName = "Utility/PlayerValue")]

public class PlayerValue : ScriptableObject
{
    [Tooltip("Trọng lực kéo player xuống")]
    public float Gravity = -9.18f * 4f;
    [Tooltip("Lớp mà player có thể nhảy")]
    public float groundDistance = 1.8f;
    [Tooltip("Tốc độ của người chơi")]
    [Range(1, 100)] public float speed = 12f;
    [Tooltip("Độ Cao nhảy Của người chơi")]
    [Range(1, 10)] public float jumpHeight = 2f;
    [Tooltip("Độ nhạy chuột/Tay cầm")]
    [Range(0.1f, 20)] public float sensitivity = 2f;
}

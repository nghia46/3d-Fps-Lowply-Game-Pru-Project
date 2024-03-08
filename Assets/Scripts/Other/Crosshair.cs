using UnityEngine;

[CreateAssetMenu(fileName = "Crosshair", menuName = "Crosshair", order = 0)]
public class Crosshair : ScriptableObject 
{
    [Header("Color")]
    [Tooltip("Color of the crosshair")]
    public Color color = Color.white;
}

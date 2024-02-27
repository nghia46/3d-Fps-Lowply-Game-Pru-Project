using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class crosshairBehaviour : MonoBehaviour
{
    [SerializeField] private Color crosshairsColor;
    [SerializeField] private Image[] crosshairs;
    private void Awake() {
        foreach(Image crosshair in crosshairs)
        {
            crosshair.color = crosshairsColor;
        }
    }
}

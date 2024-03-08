using UnityEngine;
using UnityEngine.UI;

public class CrosshairBehaviour : MonoBehaviour
{
    public static CrosshairBehaviour Instance { get; private set; }
    [SerializeField] private Crosshair crosshairValue;
    [SerializeField] private GameObject[] crosshairs;

    private void Awake()
    {
        // Ensure there's only one instance of CrosshairBehaviour
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
            return;
        }


    }
    private void FixedUpdate()
    {
        // Set crosshairs color            
        foreach (GameObject crosshair in crosshairs)
        {
            crosshair.GetComponent<Image>().color = crosshairValue.color;
        }
    }
    // Method to turn on the crosshair
    public void TurnOnCrosshair()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    // Method to turn off the crosshair
    public void TurnOffCrosshair()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
}

using TMPro;
using UnityEngine;

public class ShowHealth : MonoBehaviour
{
    private TextMeshProUGUI healthTxt;
    public GameObject EnemyInstance;
    private void Awake()
    {
        healthTxt = GetComponent<TextMeshProUGUI>();
    }
    private void LateUpdate()
    {
        healthTxt.text = "<color=red>" + EnemyInstance.GetComponent<EnemyAI>().CurrentHealth.ToString() + "</color>";
    }
}

using Entity;
using TMPro;
using UnityEngine;

public class ShowHealth : MonoBehaviour
{
    private TextMeshProUGUI healthTxt;
    public IDamageable EnemyInstance;
    private void Awake()
    {
        healthTxt = GetComponent<TextMeshProUGUI>();
        EnemyInstance = GetComponentInParent<IDamageable>();
    }
    private void LateUpdate()
    {
        healthTxt.text = "<color=red>" + EnemyInstance.GetCurrentHealth().ToString() + "</color>";
    }
}

using TMPro;
using UnityEngine;

public class ShowMaxScore : MonoBehaviour
{
    [SerializeField] private LeverValue leverValue;
    private TextMeshProUGUI txtMaxScore;
    private void Awake() {
        txtMaxScore = GetComponent<TextMeshProUGUI>();
    }
    private void Update() {
        txtMaxScore.text = $"<color=Yellow>{leverValue.Score}</color>";
    }

}


using TMPro;
using UnityEngine;


public class UpdatePoint : MonoBehaviour
{
    [SerializeField] public LeverValue value;
    private TextMeshProUGUI _txtScore;
    private void Awake()
    {
        _txtScore = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _txtScore.text = $"<color=Yellow>Score:</color> {value.Score}";
        EventManager.Instance.ScoreEvent += UpdateScore;
    }

    private void UpdateScore(int triggerId, int enemyScore)
    {
        switch (triggerId)
        {
            case 0:
                value.Score += enemyScore;
                break;
            case 1:
                value.Score += enemyScore;
                break;
            case 2:
                value.Score += enemyScore;
                break;
        }
        _txtScore.text = $"<color=Yellow>Score</color>: {value.Score}";
    }

    private void OnDisable()
    {
        EventManager.Instance.ScoreEvent -= UpdateScore;
    }
}
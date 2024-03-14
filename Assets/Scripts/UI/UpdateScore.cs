
using TMPro;
using UnityEngine;


public class UpdatePoint : MonoBehaviour
{

    [SerializeField] private int collectableId;
    [SerializeField] public int score;
    private TextMeshProUGUI _txtScore;

    private void Awake()
    {
        _txtScore = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _txtScore.text = $"<color=Yellow>Score:</color> {score}";
        EventManager.Instance.ScoreEvent += UpdateScore;
    }

    private void UpdateScore(int triggerId, int enemyScore)
    {
        switch (triggerId)
        {
            case 0:
                score += enemyScore;
                break;
            case 1:
                score += enemyScore;
                break;
            case 2:
                score += enemyScore;
                break;
        }
        _txtScore.text = $"<color=Yellow>Score</color>: {score}";
    }

    private void OnDisable()
    {
        EventManager.Instance.ScoreEvent -= UpdateScore;
    }
}
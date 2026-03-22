using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI matchText;
    [SerializeField] private TextMeshProUGUI turnText;

    private int score;
    private int matches;
    private int turns;

    private int scoreMultiplier = 1;

    void Start()
    {
        UpdateUI();
    }

    internal void GetSavedData(ScoreData scoreData)
    {
        score = scoreData.score;
        matches = scoreData.matches;
        turns = scoreData.turns;
        UpdateUI();
    }

    public void OnMatch()
    {
        AddScore();
        AddMatch();
        AddTurn();
    }

    public void OnMismatch()
    {
        AddTurn();
        scoreMultiplier = 1;
    }

    public void AddMatch()
    {
        matches++;
        UpdateUI();
    }

    public void AddTurn()
    {
        turns++;
        UpdateUI();
    }
    public void AddScore()
    {
        score += scoreMultiplier * 10;
        scoreMultiplier++;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = $"Score \n{score}";
        matchText.text = $"Matches \n{matches}";
        turnText.text = $"Turns \n{turns}";
    }

    internal int GetScore()
    {
        return score;
    }

    internal int GetMatches()
    {
        return matches;
    }

    internal int GetTurns()
    {
        return turns;
    }

    internal void ResetScore()
    {
        score = 0;
        matches = 0;
        turns = 0;
        UpdateUI();
    }
}
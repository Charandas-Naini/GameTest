using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string SAVE_KEY = "MATCH_GAME_SAVE";

    public void SaveGame(GridManager grid, ScoreSystem scoreSystem, int rows, int cols)
    {
        SaveData data = new SaveData();

        data.rows = rows;
        data.cols = cols;

        data.scoreData.score = scoreSystem.GetScore();
        data.scoreData.matches = scoreSystem.GetMatches();
        data.scoreData.turns = scoreSystem.GetTurns();

        data.cards = new List<CardData>();

        foreach (var card in grid.GetAllCards())
        {
            data.cards.Add(card.GetCardData());
        }

        string json = JsonUtility.ToJson(data);

        //Debug.Log(json);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();

        Debug.Log("Game Saved");
    }

    public SaveData LoadGame()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return null;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        Debug.Log("Game Loaded");
        return data;
    }

    public void ClearSave()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
    }

    internal bool HasSavedGame()
    {
        return PlayerPrefs.HasKey(SAVE_KEY);
    }
}
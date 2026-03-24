using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GridManager gridManager;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private GameStateManager gameStateManager;

    [SerializeField] private TMP_InputField rowInputField;
    [SerializeField] private TMP_InputField columnInputField;


    [SerializeField] private int rows = 2;
    [SerializeField] private int cols = 2;

    public Action ResetGame;

    public bool InputLocked { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void SetRows()
    {
        rows = int.Parse(rowInputField.text);
    }

    public void SetColumns()
    {
        cols = int.Parse(columnInputField.text);
    }

    public void LockInput()
    {
        InputLocked = true;
    }

    public void UnlockInput()
    {
        InputLocked = false;
    }

    public void OnMatch()
    {
        scoreSystem.OnMatch();

        if (scoreSystem.GetMatches() * 2 >= rows * cols)
        {
            gameStateManager.ChangeState(gameStateManager.GameOverState);
        }
    }

    public void OnMismatch()
    {
        scoreSystem.OnMismatch();
    }

    public void PlayGame()
    {
        gameStateManager.ChangeState(gameStateManager.GameplayState);
        gridManager.GenerateGrid(rows, cols);
        scoreSystem.ResetScore();
    }

    public void LoadGame()
    {
        gameStateManager.ChangeState(gameStateManager.GameplayState);

        var data = saveSystem.LoadGame();

        if (data != null)
        {
            gridManager.GenerateFromSave(data);
            scoreSystem.GetSavedData(data.scoreData);
        }
        else
        {
            gridManager.GenerateGrid(rows, cols);
            scoreSystem.ResetScore();
        }
    }

    public void OnResume()
    {
        gameStateManager.ChangeState(gameStateManager.GameplayState);
    }

    public void OnPause()
    {
        gameStateManager.ChangeState(gameStateManager.PauseState);
    }

    public void MainMenu()
    {
        gameStateManager.ChangeState(gameStateManager.MenuState);
        ResetGame?.Invoke();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        OnResume();
        gridManager.RestartGame();
        scoreSystem.ResetScore();
    }
    public void SaveGame()
    {
        saveSystem.SaveGame(gridManager, scoreSystem, rows, cols);
    }

}

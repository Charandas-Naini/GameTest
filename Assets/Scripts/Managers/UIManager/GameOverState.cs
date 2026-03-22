using UnityEngine;

public class GameOverState : IGameState
{
    private GameStateManager manager;
    private UIManager ui;

    public GameOverState(GameStateManager manager, UIManager ui)
    {
        this.manager = manager;
        this.ui = ui;
    }

    public void Enter()
    {
        AudioManager.Instance.PlayGameOver();
        ui.HideAll();
        ui.gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Exit()
    {
        Time.timeScale = 1f;
    }
}
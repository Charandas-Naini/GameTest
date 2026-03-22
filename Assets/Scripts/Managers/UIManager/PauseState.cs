using UnityEngine;

public class PauseState : IGameState
{
    private GameStateManager manager;
    private UIManager ui;

    public PauseState(GameStateManager manager, UIManager ui)
    {
        this.manager = manager;
        this.ui = ui;
    }

    public void Enter()
    {
        ui.pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Exit()
    {
        ui.pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
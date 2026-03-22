using UnityEngine;

public class GameplayState : IGameState
{
    private GameStateManager manager;
    private UIManager ui;

    public GameplayState(GameStateManager manager, UIManager ui)
    {
        this.manager = manager;
        this.ui = ui;
    }

    public void Enter()
    {
        ui.HideAll();
        ui.gameplayPanel.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Exit() { }
}
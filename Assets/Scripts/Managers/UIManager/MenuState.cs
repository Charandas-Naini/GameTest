public class MenuState : IGameState
{
    private GameStateManager manager;
    private UIManager ui;

    public MenuState(GameStateManager manager, UIManager ui)
    {
        this.manager = manager;
        this.ui = ui;
    }

    public void Enter()
    {
        ui.HideAll();
        ui.menuPanel.SetActive(true);
    }

    public void Exit() { }
}
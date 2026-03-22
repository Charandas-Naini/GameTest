using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    private IGameState currentState;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private Button loadButton;
    [SerializeField] private SaveSystem saveSystem;

    private MenuState menuState;
    private GameplayState gameplayState;
    private PauseState pauseState;
    private GameOverState gameOverState;


    void Awake()
    {
        menuState = new MenuState(this, uiManager);
        gameplayState = new GameplayState(this, uiManager);
        pauseState = new PauseState(this, uiManager);
        gameOverState = new GameOverState(this, uiManager);
    }

    void Start()
    {
        ChangeState(menuState);
    }

    public void ChangeState(IGameState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public IGameState MenuState
    {
        get
        {
            if (saveSystem.HasSavedGame())
            {
                loadButton.interactable = true;
            }
            else
            {
                loadButton.interactable = false;
            }
            return menuState;
        }
    }

    public IGameState GameplayState => gameplayState;
    public IGameState PauseState => pauseState;
    public IGameState GameOverState => gameOverState;
}
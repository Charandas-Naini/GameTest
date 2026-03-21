using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GridManager gridManager;
    public bool InputLocked { get; private set; }


    [SerializeField] private int rows = 2;
    [SerializeField] private int cols = 2;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gridManager.GenerateGrid(rows, cols);
    }
    public void LockInput()
    {
        InputLocked = true;
    }

    public void UnlockInput()
    {
        InputLocked = false;
    }
}

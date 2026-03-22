using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int initialCardOpenedDelay = 2;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private RectTransform container;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private CardMatchSystem cardMatchSystem;

    private List<Card> cards = new List<Card>();
    private WaitForEndOfFrame WaitForEnd = new WaitForEndOfFrame();
    private Coroutine disableGridCoroutine;

    private void Start()
    {
        GameManager.Instance.ResetGame += ResetGame;
    }

    private void OnDestroy()
    {
        GameManager.Instance.ResetGame -= ResetGame;
    }

    internal void RestartGame()
    {
        cardMatchSystem.ResetGame();
        StopDisableGridCoroutine();
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].ResetGame();
        }
        disableGridCoroutine = StartCoroutine("DisableGrid");
    }

    private void ResetGame()
    {
        ClearGrid();
    }

    public void GenerateGrid(int rows, int cols)
    {
        ClearGrid();

        int total = rows * cols;

        if (total % 2 != 0)
        {
            Debug.LogError("Total number of cards must be even to form pairs.");
            return;
        }

        EnableGrid(true);

        SetCellSize(rows, cols);

        GenerateCards(total);

        StopDisableGridCoroutine();

        disableGridCoroutine = StartCoroutine("DisableGrid");
    }

    private void StopDisableGridCoroutine()
    {
        if (disableGridCoroutine != null)
        {
            StopCoroutine(disableGridCoroutine);
        }
    }
    void ClearGrid()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        cards.Clear();
    }

    private void GenerateCards(int total)
    {
        List<int> ids = GeneratePairs(total);

        for (int i = 0; i < total; i++)
        {
            Card card = Instantiate(cardPrefab, container);

            card.Init(ids[i]);
            card.OnCardClicked += cardMatchSystem.OnCardSelected;
            card.PlayIdleOpenedAnimation();
            cards.Add(card);
        }
        GameManager.Instance.LockInput();
    }

    private void SetCellSize(int rows, int cols)
    {
        float totalSpacingX = grid.spacing.x * (cols - 1);
        float totalSpacingY = grid.spacing.y * (rows - 1);

        float cellWidth = (container.rect.width - totalSpacingX) / cols;
        float cellHeight = (container.rect.height - totalSpacingY) / rows;

        int minCardSize = 200;

        if (cellWidth > minCardSize)
        {
            cellWidth = minCardSize;
        }
        if (cellHeight > minCardSize)
        {
            cellHeight = minCardSize;
        }

        grid.cellSize = new Vector2(cellWidth, cellHeight);
        grid.constraintCount = rows;
    }

    IEnumerator DisableGrid()
    {
        yield return WaitForEnd;
        EnableGrid(false);
        yield return new WaitForSeconds(initialCardOpenedDelay);
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].PlayCloseAnimation();
        }
        GameManager.Instance.UnlockInput();
    }
    void EnableGrid(bool flag)
    {
        grid.enabled = flag;
    }

    // we assume count is always even
    List<int> GeneratePairs(int count)
    {
        List<int> ids = new List<int>();

        for (int i = 0; i < count / 2; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        Shuffle(ids);
        return ids;
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
    }
    public List<Card> GetAllCards()
    {
        return cards;
    }

    public void GenerateFromSave(SaveData data)
    {
        ClearGrid();

        EnableGrid(false);

        int total = data.cards.Count;

        int rows = data.rows;
        int cols = data.cols;

        for (int i = 0; i < total; i++)
        {
            Card card = Instantiate(cardPrefab, container);

            if (data.cards[i].isMatched)
            {
                card.SetMatched();
                card.Flip(true); // show front
            }
            else
            {
                card.OnCardClicked += cardMatchSystem.OnCardSelected;
            }

            card.Init(data.cards[i]);

            cards.Add(card);
        }
    }
}

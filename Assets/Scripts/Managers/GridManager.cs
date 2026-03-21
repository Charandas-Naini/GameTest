using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private RectTransform container;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private CardMatchSystem cardMatchSystem;

    private List<Card> cards = new List<Card>();

    public void GenerateGrid(int rows, int cols)
    {
        int total = rows * cols;

        List<int> ids = GeneratePairs(total);

        float width = container.rect.width;
        float height = container.rect.height;

        grid.cellSize = new Vector2(width / cols, height / rows);

        for (int i = 0; i < total; i++)
        {
            Card card = Instantiate(cardPrefab, container);

            card.Init(ids[i]);
            card.OnCardClicked += cardMatchSystem.OnCardSelected;

            cards.Add(card);
        }
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
}

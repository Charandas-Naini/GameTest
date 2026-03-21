using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CardMatchSystem : MonoBehaviour
{
    private List<Card> selectedCards = new List<Card>();

    public void OnCardSelected(Card card)
    {
        Debug.Log($"Card {card.GetCardId()} selected");
        selectedCards.Add(card);

        if (selectedCards.Count >= 2)
        {
            CheckMatchAsync(selectedCards[0], selectedCards[1]);
            selectedCards.RemoveRange(0, 2);
        }
    }

    private async void CheckMatchAsync(Card a, Card b)
    {
        await Task.Delay(500);

        if (a.GetCardId() == b.GetCardId())
        {
            a.SetMatched();
            b.SetMatched();
        }
        else
        {
            a.PlayCloseAnimation();
            b.PlayCloseAnimation();
        }
    }
}

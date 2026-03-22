using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class CardData
{
    public int cardId;
    public bool isFlipped;
    public bool isMatched;
    public Vector3 position;
}

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI cardText;

    public Action<Card> OnCardClicked;

    private CardAnimation cardAnimation = new CardAnimation();

    [SerializeField] private int cardId;
    [SerializeField] private bool isFlipped;
    [SerializeField] private bool isMatched;
    [SerializeField] private bool isAnimating;

    private void Awake()
    {
        cardAnimation.Init(GetComponent<Animator>());
    }

    public void Init(int id)
    {
        cardId = id;
        isFlipped = false;
        isMatched = false;
        isAnimating = false;
        cardText.text = id.ToString();
    }

    internal void ResetGame()
    {
        gameObject.SetActive(true);
        isFlipped = false;
        isMatched = false;
        isAnimating = false;
        cardText.text = cardId.ToString();
        PlayIdleOpenedAnimation();
    }
    public void Init(CardData card)
    {
        cardId = card.cardId;
        isFlipped = card.isFlipped;
        isMatched = card.isMatched;
        isAnimating = false;
        transform.position = card.position;
        cardText.text = card.cardId.ToString();

        if (isFlipped)
        {
            PlayIdleOpenedAnimation();
            OnCardClicked?.Invoke(this);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isFlipped || isMatched || isAnimating || GameManager.Instance.InputLocked) return;

        GameManager.Instance.LockInput();

        PlayOpenAnimation();

        OnCardClicked?.Invoke(this);
    }

    public void Flip(bool showFront)
    {
        isFlipped = showFront;
        AudioManager.Instance.PlayFlip();
    }

    public void SetMatched()
    {
        isMatched = true;
        gameObject.SetActive(false);
    }

    public void OnFlipAnimationEnd()
    {
        isAnimating = false;
        GameManager.Instance.UnlockInput();
    }

    public void PlayCloseAnimation()
    {
        if (isAnimating) return;

        Flip(false);
        cardAnimation.PlayCloseAnimation();
    }

    public void PlayOpenAnimation()
    {
        Flip(true);
        cardAnimation.PlayOpenAnimation();
    }

    public void PlayIdleOpenedAnimation()
    {
        cardAnimation.PlayIdleOpenedAnimation();
    }

    internal int GetCardId()
    {
        return cardId;
    }

    internal CardData GetCardData()
    {
        CardData cardData = new CardData();
        cardData.cardId = cardId;
        cardData.isFlipped = isFlipped;
        cardData.isMatched = isMatched;
        cardData.position = transform.position;
        return cardData;
    }

}

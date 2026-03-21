using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int cardId;
    [SerializeField] private bool isFlipped;
    [SerializeField] private bool isMatched;
    [SerializeField] private bool isAnimating;

    [SerializeField] private TextMeshProUGUI cardText;

    public Action<Card> OnCardClicked;

    private CardAnimation cardAnimation = new CardAnimation();

    private void Awake()
    {
        cardAnimation.Init(GetComponent<Animator>());

        isAnimating = false;
    }

    public void Init(int id)
    {
        cardId = id;
        isFlipped = false;
        isMatched = false;
        cardText.text = id.ToString();
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

    internal int GetCardId()
    {
        return cardId;
    }

}

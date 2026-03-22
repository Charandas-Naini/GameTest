using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimation
{
    private int OpenCard;
    private int CloseCard;
    private int IdleOpened;

    private Animator anime;

    internal void Init(Animator anime)
    {
        this.anime = anime;

        OpenCard = Animator.StringToHash("OpenCard");
        CloseCard = Animator.StringToHash("CloseCard");
        IdleOpened = Animator.StringToHash("IdleOpened");
    }

    internal void PlayOpenAnimation()
    {
        anime.Play(OpenCard);
    }

    internal void PlayCloseAnimation()
    {
        anime.Play(CloseCard);
    }

    internal void PlayIdleOpenedAnimation()
    {
        anime.Play(IdleOpened);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimation
{
    private int OpenCard = Animator.StringToHash("OpenCard");
    private int CloseCard = Animator.StringToHash("CloseCard");

    private Animator anime;

    internal void Init(Animator anime)
    {
        this.anime = anime;
    }

    internal void PlayOpenAnimation()
    {
        anime.Play(OpenCard);
    }

    internal void PlayCloseAnimation()
    {
        anime.Play(CloseCard);
    }

}

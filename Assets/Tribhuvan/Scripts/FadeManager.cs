using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FadeManager : MonoBehaviour
{
    public Animator fadeAnimator;

    public void FadeOut()
    {
        fadeAnimator.SetTrigger("StartFadeOut");
    }

    public void FadeIn()
    {
        fadeAnimator.SetTrigger("StartFadeIn");
    }
}

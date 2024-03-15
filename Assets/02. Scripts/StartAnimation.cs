using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    public Animator[] animators;
    int discrimination = 0;

    void Start()
    {
        StartAnimationPlay();
    }

    void StartAnimationPlay()
    {
        if (animators.Length == discrimination)
            return;

        animators[discrimination++].SetTrigger("On");
        Invoke("StartAnimationPlay", 0.3f);
    }
}

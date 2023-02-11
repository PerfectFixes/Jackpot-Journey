using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator spinningAnimator;
    private void Awake()
    {
        spinningAnimator = gameObject.GetComponent<Animator>();
    }

    public void NextAnimation()
    {
        //Randomizing an animation to play
        int nextAnimtion = Random.Range(1, 7);

        if (nextAnimtion == 1)
        {
            spinningAnimator.Play("Spinning_Animation_1");
        }
        else if (nextAnimtion == 2)
        {
            spinningAnimator.Play("Spinning_Animation_2");
        }
        else if (nextAnimtion == 3)
        {
            spinningAnimator.Play("Spinning_Animation_3");
        }
        else if (nextAnimtion == 4)
        {
            spinningAnimator.Play("Spinning_Animation_4");
        }
        else if (nextAnimtion == 5)
        {
            spinningAnimator.Play("Spinning_Animation_5");
        }
        else
        {
            spinningAnimator.Play("Spinning_Animation_6");
        }
    }
}

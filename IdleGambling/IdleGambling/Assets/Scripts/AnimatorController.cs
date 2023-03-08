using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator spinningAnimator;
    public bool isStop;
    public bool isInCycle;
    public int iconNumber;
    string clipName;

    [Tooltip("The SFX for the first slot")]
    [SerializeField] private AudioSource firstSlotSFX;

    [Tooltip("The SFX for the second slot")]
    [SerializeField] private AudioSource secondSlotSFX;

    [Tooltip("The SFX for the third slot")]
    [SerializeField] private AudioSource thirdSlotSFX;

    private void Awake()
    {
        
        isStop = false;
        spinningAnimator = gameObject.GetComponent<Animator>();
        spinningAnimator.speed = Random.Range(1.1f, 1.6f);
    }
    public void NextAnimation()
    {
 
        if (isStop)
        {
            spinningAnimator.Play(iconNumber.ToString());
        }
        else
        {
       
            if (isInCycle)
            {
                isInCycle = false;
                //Randomizing an animation to play
                int nextAnimation = Random.Range(1, 13);
                spinningAnimator.Play(nextAnimation.ToString());
            }            
            
        }
        AnimatorClipInfo[] clipInfo = spinningAnimator.GetCurrentAnimatorClipInfo(0);
        clipName = clipInfo[0].clip.name;
    }
    public void FreezeAnimation()
    {
        if (isStop)
        {
            if (clipName == iconNumber.ToString())
            {
                PlaySFX();
                spinningAnimator.speed = 0;
                spinningAnimator.Play(iconNumber.ToString(), -1, 0.5f);

            }
            
        }
        
    }
    public void FinishCycle()
    {
        isInCycle = true;
    }
    private void PlaySFX()
    {
        if(gameObject.name == "First_Slot" && firstSlotSFX.isActiveAndEnabled)
        {
            firstSlotSFX.Play();
        }
        else if (gameObject.name == "Second_Slot" && secondSlotSFX.isActiveAndEnabled)
        {
            secondSlotSFX.Play();
        }
        else if (gameObject.name == "Third_Slot" && thirdSlotSFX.isActiveAndEnabled)
        {
            thirdSlotSFX.Play();
        }
    }
    public void StopAnimation(bool isStop)
    {
        if (isStop)
        {
            this.isStop = true;
        }
        else
        {
            this.isStop = false;
            spinningAnimator.speed = Random.Range(1.2f, 1.6f);
        }
    }
}

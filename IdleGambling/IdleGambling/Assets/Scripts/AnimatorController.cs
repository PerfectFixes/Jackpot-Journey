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

        //Setting a random speed of cycle
        spinningAnimator.speed = Random.Range(1.1f, 1.6f);
    }
    public void NextAnimation()
    {
        //If the has stopped sets the currect icon to display
        if (isStop)
        {
            spinningAnimator.Play(iconNumber.ToString());
        }
        else
        {
            //If the animator is playing, sets a random image and rerun the animation
            if (isInCycle)
            {
                isInCycle = false;

                //Randomizing an animation to play
                int nextAnimation = Random.Range(1, 14);
                spinningAnimator.Play(nextAnimation.ToString());
            }            
            
        }
        //Checks what is the name of the current clip
        AnimatorClipInfo[] clipInfo = spinningAnimator.GetCurrentAnimatorClipInfo(0);
        clipName = clipInfo[0].clip.name;
    }
    public void FreezeAnimation()
    {
        //If the player wanted to stop the gamble
        if (isStop)
        {
            //Verify that the current image when the player stopped the machine is the correct image
            if (clipName == iconNumber.ToString())
            {
                PlaySFX();
                spinningAnimator.speed = 0;
                spinningAnimator.Play(iconNumber.ToString(), -1, 0.5f);

            }
            
        }
        
    }
    //Setting this value to true
    public void FinishCycle()
    {
        isInCycle = true;
    }
    //Playing the SFX when the machine is stopping
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
    //Stops the animation in the currect place
    public void StopAnimation(bool isStop)
    {
        //Telling the animation to stop in the next cycle
        if (isStop)
        {
            this.isStop = true;
        }
        //Resume the animation in random speed
        else
        {
            this.isStop = false;
            spinningAnimator.speed = Random.Range(1.2f, 1.6f);
        }
    }
}

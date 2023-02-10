using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayingTheWinOrLose : MonoBehaviour
{
    [Header("The slot position")]
    [SerializeField] private SpriteRenderer firstSlot;
    [SerializeField] private SpriteRenderer secondSlot;
    [SerializeField] private SpriteRenderer thirdSlot;

    [Header("The color of the gambling reward/lose")]
    [SerializeField] private Color losingColor;
    [SerializeField] private Color smallWinColor;
    [SerializeField] private Color mediumWinColor;
    [SerializeField] private Color bigWinColor;
    [SerializeField] private Color blockedColor;
    [SerializeField] private Color defaultColor;

    public IEnumerator DisplayingTheLose()
    {
        firstSlot.color = blockedColor;
        secondSlot.color = blockedColor;
        thirdSlot.color = blockedColor;
        yield return new WaitForSeconds(0.5f);
        // ----- Making the blocked a bit gray -----
        firstSlot.color = losingColor;
        yield return new WaitForSeconds(0.25f);
        secondSlot.color = losingColor;
        yield return new WaitForSeconds(0.25f);
        thirdSlot.color = losingColor;
    }
    public IEnumerator DisplayTheWin(int result)
    {
   
        switch (result)
        {
            case 60:
                firstSlot.color = blockedColor;
                secondSlot.color = blockedColor;
                thirdSlot.color = blockedColor;
                yield return new WaitForSeconds(0.5f);
                // ----- Making the blocked a bit gray -----
                firstSlot.color = smallWinColor;
                yield return new WaitForSeconds(0.25f);
                secondSlot.color = smallWinColor;
                yield return new WaitForSeconds(0.25f);
                thirdSlot.color = smallWinColor;
                
                break;
//-------------------------------------------------
            case 30:
                firstSlot.color = blockedColor;
                secondSlot.color = blockedColor;
                thirdSlot.color = blockedColor;
                yield return new WaitForSeconds(0.5f);
                // ----- Making the blocked a bit gray -----
                firstSlot.color = mediumWinColor;
                yield return new WaitForSeconds(0.25f);
                secondSlot.color = mediumWinColor;
                yield return new WaitForSeconds(0.25f);
                thirdSlot.color = mediumWinColor;
                break;
//-------------------------------------------------
            case 10:
                firstSlot.color = blockedColor;
                secondSlot.color = blockedColor;
                thirdSlot.color = blockedColor;
                yield return new WaitForSeconds(0.5f);
                // ----- Making the blocked a bit gray -----
                firstSlot.color = bigWinColor;
                yield return new WaitForSeconds(0.25f);
                secondSlot.color = bigWinColor;
                yield return new WaitForSeconds(0.25f);
                thirdSlot.color = bigWinColor;
                break;

        }
    }
    public void ReadyToGamble()
    {
        //Setting the color to White
        firstSlot.color = defaultColor;
        secondSlot.color = defaultColor;
        thirdSlot.color = defaultColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupControl : MonoBehaviour
{
    private IronSourceScript ironSourceScript;
    [SerializeField] private GameObject autoClickerGameObject;
    [SerializeField] private GameObject increaseLuckGameObject;
    [SerializeField] private Animator errorMessageAnimator;

    private readonly TMP_Text errorMessageText;

    void Start()
    {
        ironSourceScript = GameObject.Find("IronSource").GetComponent<IronSourceScript>();
    }

    //Open the popup
    public void PopupOpen(string popupName)
    {
        //Checks to see which popup it is
        if (popupName == "Auto Clicker")
        {

            autoClickerGameObject.SetActive(true);
        }
        else 
        {
            increaseLuckGameObject.SetActive(true);
        }
    }
    //Close the popup
    public void PopupClose(string popupName)
    {
        //Checks to see which popup it is
        if (popupName == "Auto Clicker")
        {
            autoClickerGameObject.SetActive(false);
            
        }
        else
        {
            increaseLuckGameObject.SetActive(false);
        }
    }
    //Play the ad if there is internet
    public void PlayAd(string adType)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //if there is no internet display the no internet error
            autoClickerGameObject.SetActive(false);
            increaseLuckGameObject.SetActive(false);
            errorMessageAnimator.Play("Error_Message_Display");
            errorMessageText.text = "No internet connection";
        }
        else
        {
            //Play the ad if there is internet
            ironSourceScript.ShowRewardedAd(adType);
            autoClickerGameObject.SetActive(false);
            increaseLuckGameObject.SetActive(false);
        }

    }
}

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
    // Start is called before the first frame update
    void Start()
    {
        ironSourceScript = GameObject.Find("IronSource").GetComponent<IronSourceScript>();
    }

    public void PopupOpen(string popupName)
    {
        if (popupName == "Auto Clicker")
        {

            autoClickerGameObject.SetActive(true);
        }
        else 
        {
            increaseLuckGameObject.SetActive(true);
        }
    }
    public void PopupClose(string popupName)
    {
        if (popupName == "Auto Clicker")
        {
            autoClickerGameObject.SetActive(false);
            
        }
        else
        {
            increaseLuckGameObject.SetActive(false);
        }
    }
    public void PlayAd(string adType)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {

            autoClickerGameObject.SetActive(false);
            increaseLuckGameObject.SetActive(false);
            errorMessageAnimator.Play("Error_Message_Display");
            errorMessageText.text = "No internet connection";
        }
        else
        {
            ironSourceScript.ShowRewardedAd(adType);
            autoClickerGameObject.SetActive(false);
            increaseLuckGameObject.SetActive(false);
        }

    }
}

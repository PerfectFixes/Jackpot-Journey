using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupControl : MonoBehaviour
{
    private IronSourceScript ironSourceScript;
    [SerializeField] private GameObject autoClickerGameObject;
    [SerializeField] private GameObject increaseLuckGameObject;
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
        ironSourceScript.ShowRewardedAd(adType);
        autoClickerGameObject.SetActive(false);
        increaseLuckGameObject.SetActive(false);
    }
}

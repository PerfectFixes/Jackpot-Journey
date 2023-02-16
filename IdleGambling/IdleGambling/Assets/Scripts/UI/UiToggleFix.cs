using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiToggleFix : MonoBehaviour
{
    [SerializeField] private Image spriteColor;
    [SerializeField] private Toggle isToggleOn;

    private void Awake()
    {
        if (isToggleOn.isOn)
        {
            spriteColor.color = new Color(255, 255, 255, 0);
        }
        else
        {
            spriteColor.color = new Color(255, 255, 255, 255);
        }
    }
    public void ToggleIsOn()
    {
        if (isToggleOn.isOn)
        {
            spriteColor.color = new Color(255, 255, 255, 0);
        }
        else
        {
            spriteColor.color = new Color(255, 255, 255, 255);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAnimation : MonoBehaviour
{
    //Disables the gameobject
    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}

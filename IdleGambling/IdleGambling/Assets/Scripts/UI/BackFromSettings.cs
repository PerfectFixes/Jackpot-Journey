using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackFromSettings : MonoBehaviour
{
    [SerializeField] private Animator sceneLoader;

    public void LoadScene()
    {
        StartCoroutine(SceneLoader());
    }
    IEnumerator SceneLoader()
    {
        sceneLoader.SetTrigger("Load_Scene");
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene("Game_Scene");
    }
}

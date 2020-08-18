using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public string sceneName = "";

    private void Update()
    {
    }

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

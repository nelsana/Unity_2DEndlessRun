using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public int sceneNum;

    public void GoToNextScene()
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void QuitScene()
    {
        Application.Quit();
    }
}

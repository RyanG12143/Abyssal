using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private string prevScene;
    public void exit()
    {
        Application.Quit();
    }
    public void toStart()
    {
        prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(0);
        SceneManager.UnloadSceneAsync(prevScene);
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
}

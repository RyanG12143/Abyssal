using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public void respawn()
    {
        Debug.Log("respawn");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        gameObject.GetComponent<ShowHide>().Hide();
        Time.timeScale = 1;
    }

    public void rageQuit()
    {
        Application.Quit();
    }
}

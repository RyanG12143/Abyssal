using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    
    public void startGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(1);
    }
}

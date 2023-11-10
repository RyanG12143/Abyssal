using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class BreakingWallsTest
{
    
    // A Test behaves as an ordinary method
    [Test]
    public void BreakingWallsTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator BreakingWallsTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        SceneManager.LoadScene("Water_Current_Test");
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject wall = GameObject.FindGameObjectWithTag("CrackedWall");
        Fire fireScript = Player.GetComponent<Fire>();
        

        yield return null;
    }
}

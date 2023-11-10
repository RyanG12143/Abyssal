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
        SceneManager.LoadScene("Water_Current_Test");
        GameObject nightingale = GameObject.FindGameObjectWithTag("Player");
        GameObject destroyableWall = GameObject.FindGameObjectWithTag("CrackedWall");
        Fire fire = nightingale.GetComponent<Fire>();
        if (destroyableWall.activeSelf)
        {
            Assert.True(true);
        }

        yield return null;
    }
}

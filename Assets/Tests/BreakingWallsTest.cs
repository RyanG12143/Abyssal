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
        nightingale = MonoBehaviour.Instantiate(nightingale);
        GameObject destroyableWall = GameObject.FindGameObjectWithTag("CrackedWall");
        destroyableWall = MonoBehaviour.Instantiate(destroyableWall);
        Fire fire = nightingale.GetComponent<Fire>();
        destroyableWall.transform.position = new Vector2(5.32f, 2.46f);
        Assert.That(destroyableWall.transform.position, Is.EqualTo(new Vector2(5.32f, 2.46f)));

        yield return null;
    }
}

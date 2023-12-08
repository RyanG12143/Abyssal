using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MatthewTest : InputTestFixture
{
    // A Test behaves as an ordinary method
    [Test]
    public void MatthewTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    Mouse mouse;
    public override void Setup()
    {
        SceneManager.LoadScene("Scenes/ReefLevel");
        base.Setup();

        mouse = InputSystem.AddDevice<Mouse>("mouse");

        Press(mouse.leftButton);
        Release(mouse.leftButton);

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MatthewTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Press(mouse.leftButton);
        yield return new WaitForSeconds(5);
        Release(mouse.leftButton);
        yield return new WaitForSeconds(5);

        GameObject torpedo = GameObject.FindGameObjectWithTag("Torpedo");
        Assert.IsNotNull(torpedo);
        yield return null;
    }
}

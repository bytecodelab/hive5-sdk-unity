/**
 * @file TestRunner.cs
 * 
 * Unity3D unit test runner.
 * Sets up the unit testing suite and executes all unit tests.
 * Drag this onto an empty GameObject to run tests.
 */

using UnityEngine;
using System.Collections;
using SharpUnit;
using Hive5;
using Hive5.model;
using System.Collections.Generic;

public class Unity3D_TestRunner : MonoBehaviour 
{
    /**
     * Initialize class resources.
     */
	void Start() 
    {
        // Create test suite
        TestSuite suite = new TestSuite();

        // Example: Add tests to suite
        //suite.AddAll(new Dummy_Test());
		suite.AddAll (new SampleTestCase ());

//		StartCoroutine(TestItems ());

        // Run the tests
        TestResult res = suite.Run(null);

        // Report results
        Unity3D_TestReporter reporter = new Unity3D_TestReporter();
        reporter.LogResults(res);
	}

	IEnumerator TestItems() 
	{
		Hive5Client client = new Hive5Client ("a4b9dff4-3a9a-402b-bc2b-00d144ebacad", "0f607264fc6318a92b9e13c65db7cd3c", "e7c30d94-126a-440c-9ea6-40e5ebe37ae5");
		
//		Dictionary<string, Item> result = null;
//		var a = 0;
		return client.GetItems (new string[] { "items." }, x => Debug.Log("end"));

//		return TestItemsImpl (x => Debug.Log(x));
	}

	IEnumerator TestItemsImpl(System.Action<string> result)
	{
		return TestItemsImpl2(x => result("good" + x));

	}

	IEnumerator TestItemsImpl2(System.Action<string> result)
	{
		WWW www = new WWW ("http://google.com");
		yield return www;
		
		result (www.text);
	}

}

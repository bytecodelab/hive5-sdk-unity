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
//		suite.AddAll (new SampleTestCase ());

		var client = new Hive5Client ("a4b9dff4-3a9a-402b-bc2b-00d144ebacad", "0f607264fc6318a92b9e13c65db7cd3c", "e7c30d94-126a-440c-9ea6-40e5ebe37ae5");

		Dictionary<string, Item> result = null;
		StartCoroutine(client.GetItems (new string[] {"items."}, x => 
		{
			result = x;
			Debug.Log (result.Count);
			Debug.Log("============End");

			result = x;
		}));


        // Run the tests
        TestResult res = suite.Run(null);

        // Report results
        Unity3D_TestReporter reporter = new Unity3D_TestReporter();
        reporter.LogResults(res);
	}



}

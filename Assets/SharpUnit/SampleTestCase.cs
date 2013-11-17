using System;
using SharpUnit;
using UnityEngine;
using Hive5;
using Hive5.model;
using System.Collections.Generic;
using System.Collections;
using System.Threading;

class SampleTestCase : TestCase
{
	Hive5Client client = new Hive5Client ("a4b9dff4-3a9a-402b-bc2b-00d144ebacad", "0f607264fc6318a92b9e13c65db7cd3c", "e7c30d94-126a-440c-9ea6-40e5ebe37ae5");
	
	[UnitTest]
	public void TestGetItems ()
	{

		var result = GetItems (client, new string[] { "items." });

		Assert.True (result.Count == 0);
		Debug.Log ("Not yet");


	}

	Dictionary<string, Item> GetItems(Hive5Client client, string [] keys) 
	{
		Dictionary<string, Item> result = null;

		ManualResetEvent mre = new ManualResetEvent (false);
//		client.GetItems (keys, x => {
		temp(x => {
			result = x;
			mre.Set ();
			Debug.Log("abc");
		});

		mre.WaitOne (3000);

		return result;
	}

	IEnumerator temp(Action<Dictionary<string, Item>> result) 
	{
		yield return null;
		result (new Dictionary<string, Item>());
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hive5;
using Hive5.model;
using UnTest;
using System.Threading;
using System;

// all classes marked with TestSuite get tested 
//[TestSuite]
//class MyTestSuite {
//	
//	private Hive5Client client;
//	
//	// called before every test        
//	[TestSetup]
//	void DoSetup() {
//		client = new Hive5Client ("a4b9dff4-3a9a-402b-bc2b-00d144ebacad", "0f607264fc6318a92b9e13c65db7cd3c", "e7c30d94-126a-440c-9ea6-40e5ebe37ae5");
//	}
//	
//	// any exception is a test failure
//	[Test]
//	void GetResult_WithValidResult_GivesResult() {
////		ManualResetEvent resetEvent = new ManualResetEvent(false);
//
////		var e = client.GetItems (new string[] { "items." }, x => {
////			Assert.IsTrue(x.Count != 0); 
////			resetEvent.Set ();
////			Debug.Log ("==================End!");
////		});
//
//
//
//		Debug.Log ("==================Not yet!");
////		resetEvent.WaitOne ();
//	}
//
//	IEnumerator temp(Action<Dictionary<string, Item>> result) 
//	{
//		yield return null;
//		result (new Dictionary<string, Item>());
//	}
//	
//}
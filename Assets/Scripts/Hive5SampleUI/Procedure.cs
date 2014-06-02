using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Model;
using Hive5.Util;

public class Procedure : MonoBehaviour {
	
	Hive5Client hive5;
	
	public void callProcedure()
	{
		hive5 = Hive5Client.Instance;
		var parameters = new TupleList<string, string> ();
		
		hive5.CallProcedure("get_user_name", parameters, response => {
			Debug.Log ("onCallProcedure");
			Debug.Log ("result data = "+response.resultData);
		});
		
	}
	
}

using UnityEngine;
using System.Collections;
 
public class DragGameObject : MonoBehaviour {
	
	public Camera cam;
	
	bool isDown = false;
	Vector3 scrSpace;
	Vector3 offset;
	
	void Update()
	{
		if(!isDown)
		{
			if(Input.GetMouseButtonDown(0))
			{
		        scrSpace = cam.WorldToScreenPoint (transform.position);
		        offset = transform.position - cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, scrSpace.z));
				isDown = true;
			}
			
		} else {
			
			if(Input.GetMouseButton(0))
			{
	            Vector3 curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, scrSpace.z);
    	        Vector3 curPosition = cam.ScreenToWorldPoint(curScreenSpace) + offset;
        	    transform.position = curPosition;				
				
			} else {
				
				isDown = false;
				
			}
		}
		
	}

}

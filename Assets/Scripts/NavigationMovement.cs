using UnityEngine;
using System.Collections;

public class NavigationMovement : MonoBehaviour 
{
	float lat;
	float lon; 

	public float accuracyInMeters = 1.0f;
	public float updateDistanceInMeters = 1.0f;

	void Start() 
	{
		if (Input.location.isEnabledByUser)
		{
			Input.location.Start(accuracyInMeters,updateDistanceInMeters);
			LocationInfo li;
			li = Input.location.lastData;
			lat = li.latitude;
			lon = li.longitude;
		}
		else
		{
			Debug.LogError("GPS is off, using terrible phone service");
			return;

			Input.location.Start(500.0f,500.0f);
			LocationInfo li;
			li = Input.location.lastData;
			lat = li.latitude;
			lon = li.longitude;
		}
	}
	
	void Update() 
	{
		if (Input.location.status == LocationServiceStatus.Running)
		{
			LocationInfo li;
			li = Input.location.lastData;

			double delta = MercatorProjection.latToY(li.latitude) - MercatorProjection.latToY(lat);
			transform.position += transform.forward * (float)delta * Time.deltaTime;
			delta = MercatorProjection.lonToX(li.longitude) - MercatorProjection.lonToX(lon);
			transform.position += transform.right * (float)delta * Time.deltaTime;
			
			lat = li.latitude;
			lon = li.longitude;
		}
	}
	
	void OnGUI()
	{
		GUI.Button(new Rect (10,10,512,60), " LAT = "+ lat +" LON = " + lon);
		GUI.Button(new Rect (10,70,512,60), "STAT = " + Input.location.status + " ON = " + Input.location.isEnabledByUser);
		GUI.Button(new Rect(10,140,512,60), "Transform : " +  transform.position.x + " : " + transform.position.z);
	}

	void OnDisable()
	{
		Input.location.Stop();
	}
}

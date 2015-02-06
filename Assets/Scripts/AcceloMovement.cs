using UnityEngine;
using System.Collections;

public class AcceloMovement : MonoBehaviour 
{
	void Update() 
	{
		Input.gyro.enabled = true;
		transform.position += AccelorometerDirection() * Time.deltaTime;
	}
	
	Vector3 AccelorometerDirection()
	{
		return Input.gyro.userAcceleration;
	}
}

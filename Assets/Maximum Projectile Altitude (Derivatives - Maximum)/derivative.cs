using UnityEngine;
using System.Collections;

public class derivative : MonoBehaviour {
	
	void Start () {
	}

	void Update () {

	}

	public void SetVelocity(Vector3 velocity)
	{
		rigidbody.velocity = velocity;
	}
}

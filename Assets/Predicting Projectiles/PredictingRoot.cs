using UnityEngine;
using System.Collections;

public class PredictingRoot : MonoBehaviour {

	public GameObject item;
	public Vector3 position = Vector3.zero;
	public Vector3 velocity = Vector3.zero;
	public float time = 0f;
	public float interval = 0.1f;

	public float beginingTime = 1.0f;
	public float endingTime = 2.0f;

	Vector3 gravity;

	public float gravityY = -9.81f;

	float currentInterval = 0f;
	void Start () {
		currentInterval = beginingTime;
		gravity = new Vector3 (0f, gravityY, 0f);
	}

	void Update() {
		if (Time.time >= beginingTime && Time.time <= endingTime) {
			if(Time.time >= currentInterval)
			{
				currentInterval += interval;
				Vector3 newPosition = PredictProjectileAtTime (currentInterval, velocity, position, gravity); 
				Create(currentInterval, newPosition);
			}
		}
	}

	void Create(float time, Vector3 position) {
		GameObject go = Instantiate (item, position, Quaternion.identity) as GameObject;
		go.name = "Time - " + time.ToString () + " second";
		go.transform.parent = transform;
	}

	Vector3 PredictProjectileAtTime(float t, Vector3 v0, Vector3 x0, Vector3 g)
	{
		return g * (0.5f * t * t) + v0 * t + x0;
	}
}

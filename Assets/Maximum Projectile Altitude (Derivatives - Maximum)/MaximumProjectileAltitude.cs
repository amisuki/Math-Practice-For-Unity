using UnityEngine;
using System.Collections;

public class MaximumProjectileAltitude : MonoBehaviour {
		
	public GameObject item;
	public GameObject derivativeGO;

	public Vector3 position = Vector3.zero;
	public Vector3 velocity = Vector3.zero;
	public float time = 0f;
	public float interval = 0.1f;
	
	public float beginingTime = 0f;
	public float endingTime = 2.0f;
	
	Vector3 gravity;
	
	public float gravityY = -9.81f;

	//공중에서 폭파 시간
	float breakTime;
	float currentInterval = 0f;

	public int derivativeMax = 10;
	Vector3 lastPosition;
	Vector3 lastVelocity;
	void Start () {
		Random.seed = System.DateTime.Today.Day;
		currentInterval = beginingTime;
		gravity = new Vector3 (0f, gravityY, 0f);
		breakTime = beginingTime + PredictProjectileMaximumHeightTime(velocity, gravity);
	}
	
	void Update() {
		if (Time.time >= beginingTime && Time.time <= endingTime) {
			lastPosition = PredictProjectileAtTime (Time.time, velocity, position, gravity);
			transform.position = lastPosition;
			lastVelocity += velocity * Time.deltaTime;
		}

		if (Time.time >= breakTime && breakTime != -1) {
			breakTime = -1;
			for(int i = 0; i < derivativeMax; ++i) {
				GameObject go = Instantiate(derivativeGO, lastPosition, Quaternion.identity) as GameObject;
				go.name = i.ToString();
				derivative der = go.GetComponent<derivative>();
				Vector3 newVelocity = new Vector3((float)Random.Range(0, 1000)/250-2, (float)Random.Range(0, 1000)/250-2, (float)Random.Range(0, 1000)/250-2);

				newVelocity = newVelocity + velocity;
				newVelocity.y = 0f;

				Debug.Log("i : " + i + " : " + newVelocity);
				der.SetVelocity(newVelocity);
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

	float PredictProjectileMaximumHeightTime(Vector3 v0, Vector3 g)
	{
		return -v0.y / g.y;
	}

}



using UnityEngine;
using System.Collections;

public class fallingDetection : MonoBehaviour {

	public GameObject falling_label;
	// Use this for initialization
	Vector3 InitialPos;
	float msgTime = 10 / Time.deltaTime;
	int fallingTime = 0;

	void Start () {
		InitialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {	
		if (transform.position.y < -20)
		{
			falling_label.SetActive(true);

				transform.position = InitialPos;
				rigidbody.velocity = Vector3.zero;
				fallingTime = 0;

		}
		else
			falling_label.SetActive(false);
	}
}

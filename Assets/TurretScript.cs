using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {
    public float reloadSpeed = 10;
    public float cannonballSpeed = 30;
    public float accuracyRadius = 10;
    public bool HighArcTrajectory = false;
    public bool isTargeted = false;
    float reloadCounter = 0;

    public GameObject cannonBall;
	bool isOn = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Turn cannon on or off
		if (Input.GetKeyDown("o") && isTargeted )
			isOn = !isOn;
        // For case where not targeted anymore, but was left on
        if (!isTargeted)
            isOn = false;

        reloadCounter += Time.deltaTime;
		if (reloadCounter >= reloadSpeed && isOn ){
            GameObject ball = (GameObject)GameObject.Instantiate(cannonBall, transform.position, Quaternion.LookRotation(Vector3.up));
            ball.tag = "CannonBall";
            ball.GetComponent<BulletMovement>().speed = cannonballSpeed;
            ball.GetComponent<BulletMovement>().highArc = HighArcTrajectory;
            ball.GetComponent<BulletMovement>().targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up + Random.insideUnitSphere * accuracyRadius;
			// Because Tony likes pretty stuff!
			ball.renderer.material.color = new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
			reloadCounter = 0;
        }
	}

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            gameObject.SetActive(false);
            //transform.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

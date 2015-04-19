using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public int lives = 3;

    public float immuneTimeAfterHit = 1.0f;
    float immuneTimeCounter = 0;
    bool immune = false;
    Vector3 initPos = new Vector3(0,0,0);

	// List of all turrets/visible turrets
	GameObject[] AllTurrets; 
	GameObject CurrentTurret;
	int TurPos = 0; // I'm so sorry Marky Mark

    // List of all CannonBalls;
    GameObject[] AllCannons;

	// Use this for initialization
	void Start () {
        AllTurrets = GameObject.FindGameObjectsWithTag("Turret");
        CurrentTurret = AllTurrets[0];
        CurrentTurret.GetComponent<TurretScript>().isTargeted = true;
        CurrentTurret.renderer.material.color = new Color(.2f, .3f, 1f);
        initPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (immune){
            immuneTimeCounter += Time.deltaTime;

            //Makes the player flash when he gets hit
            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++ ){
                renderers[i].enabled = ((int)(immuneTimeCounter * 12)) % 2 == 0;
            }

			//Makes sure the player is visible when done with the flashing
            //and immunity.
            if (immuneTimeCounter >= immuneTimeAfterHit){
                for (int i = 0; i < renderers.Length; i++ ){
                    renderers[i].enabled = true;
                }
                immune = false;
                immuneTimeCounter = 0;
            }
        }

        // I'm faaaaaaaaaaaaaaaaaaaaaaaaaaaallllllllllllllliiiiiinnnnnnnnggggg
        if (transform.position.y < -5)
        {
            transform.rigidbody.velocity = new Vector3(0, 0, 0);
            transform.position = initPos;
            // haha, like it matters
            lives--;
        }

		// Change Firing turret
		if (Input.GetKeyDown ("tab"))
			ChangeTurret();

        if (Input.GetKeyDown("r"))
        {
            AllCannons = GameObject.FindGameObjectsWithTag("CannonBall");
            DeleteCannons();
        }
	}

	void ChangeTurret()
	{
        // Previous turret detargeted
        AllTurrets[TurPos].GetComponent<TurretScript>().isTargeted = false;
        AllTurrets[TurPos].renderer.material.color = new Color(1.0f, 1.0f, 1.0f);
		if (TurPos + 1 < AllTurrets.Length)     
			CurrentTurret = AllTurrets [++TurPos];
        else
            CurrentTurret = AllTurrets[TurPos = 0];
        // Current turret targeted
        CurrentTurret.GetComponent<TurretScript>().isTargeted = true;
        CurrentTurret.renderer.material.color = new Color(1f, .1f, .15f);
	}

    void DeleteCannons()
    {
        for (int i = 0; i < AllCannons.Length; i++)
        {
            AllCannons[i].GetComponent<BulletMovement>().ClearBullet();
        }
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.tag == "DamageZone" && !immune){
            lives--;
            immune = true;
        }
    }

    public void TakeDamage(){
        if(!immune){
            lives--;
            immune = true;
        }
    }
}

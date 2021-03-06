﻿using UnityEngine;
using System.Collections;

public class star_hit : MonoBehaviour {

	public float weaponDamage;

	projectileController myPC;

	public GameObject blood_splatter;

	// Use this for initialization
	void Awake () {
		myPC = GetComponentInParent<projectileController> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer == LayerMask.NameToLayer ("Shootable")) {
			myPC.removeForce ();
			Instantiate (blood_splatter, transform.position, transform.rotation);
			Destroy (gameObject);
			if (other.tag == "Enemy") {
				enemyHealth hurtEnemy = other.gameObject.GetComponent<enemyHealth> ();
				hurtEnemy.addDamage (weaponDamage);
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.layer == LayerMask.NameToLayer ("Shootable")) {
			myPC.removeForce ();
			Instantiate (blood_splatter, transform.position, transform.rotation);
			Destroy (gameObject);
			if (other.tag == "Enemy") {
				enemyHealth hurtEnemy = other.gameObject.GetComponent<enemyHealth> ();
				hurtEnemy.addDamage (weaponDamage);
			}
		}
	}
}

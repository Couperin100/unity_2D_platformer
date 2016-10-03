using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

	public float fullHealth;
	public GameObject deathFX;
	public AudioClip[] playerHurt;

	float currentHealth;

	PlayerController controlMovement;

	AudioSource playerAS;

	//HUD Variables
	public Slider healthSlider;
	public Image damageScreen;

	bool damaged = false;
	Color damagedColour = new Color(0f,0f,0f,1f);
	float smoothColour = 5f;

	// Use this for initialization
	void Start () {
		currentHealth = fullHealth;
		controlMovement = GetComponent<PlayerController> ();

		//HUD Initialization
		healthSlider.maxValue = fullHealth;
		healthSlider.value = fullHealth;

		damaged = false;

		playerAS = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (damaged) {
			damageScreen.color = damagedColour;
		} else {
			damageScreen.color = Color.Lerp (damageScreen.color, Color.clear, smoothColour * Time.deltaTime);
		}
		damaged = false;


	}

	public void addDamage(float damage){
		if (damage <= 0) return;
		currentHealth -= damage;

//		playerAS.clip = playerHurt;
//		playerAS.Play ();
		AudioClip tempClip = playerHurt[Random.Range(0,playerHurt.Length)];
		playerAS.clip = tempClip;
		playerAS.Play();﻿

		damaged = true;
		healthSlider.value = currentHealth;

		if(currentHealth<=0){
			makeDead ();

		}
	}

	public void makeDead(){
		Instantiate (deathFX,transform.position, transform.rotation);
		//Debug.Log("DYING");
		Destroy(gameObject);
		//Application.LoadLevel(Application.loadedLevel);
		//Task.Delay(1000).ContinueWith(t=> Application.LoadLevel(Application.loadedLevel));

	}
		
}

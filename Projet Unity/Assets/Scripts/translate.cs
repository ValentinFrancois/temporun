using UnityEngine;
using System.Collections;

public class translate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if (!GameObject.Find("GameOver").GetComponent<end>().mort){
		if (!GameObject.Find("Perso").GetComponent<PersoController>().mort){
			transform.Translate (/*-0.130f*/ -3.85f*Time.deltaTime, 0, 0); 
			if (transform.position.x < -20) {
				Destroy (this.gameObject);
			}
		}

	}


}

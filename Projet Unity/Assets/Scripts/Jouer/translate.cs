using UnityEngine;
using System.Collections;

public class translate : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
		
		if (!GameObject.Find("Perso").GetComponent<PersoController>().mort){
			transform.Translate (/*-0.130f*/ -4f*Time.deltaTime, 0, 0); 
			if (transform.position.x < -20) {
				Destroy (this.gameObject);
			}
		}

	}

}
